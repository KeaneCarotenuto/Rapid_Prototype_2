using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    [Header("Movement")]
    public float m_moveSpeed = 1.0f;

    [SerializeField] bool manual_height;

    public float yTarget = 1.25f;
    public Vector2 yBounds;
    public Vector2 xBounds;
    public Vector2 zBounds;

    [SerializeField] Animator anim;


    [Header("Grabbing")]
    bool mouseDown = false;

    public Rigidbody m_rb;
    public ConfigurableJoint m_joint;

    public float grabDistance;
    public LayerMask grabbable;

    public GameObject[] fingers;

    public Transform grabSpot;

    [Header("Rage")]
    [SerializeField] float rageAmount = 0;

    [SerializeField] GameObject axe;

    [SerializeField] TaskManager m_taskManager;


    private void OnDrawGizmos()
    {
        Vector3 blfCorner = new Vector3(xBounds.x, yBounds.x, zBounds.x);
        Vector3 trbCorner = new Vector3(xBounds.y, yBounds.y, zBounds.y);

        Gizmos.DrawWireCube(
            (blfCorner + trbCorner) /2.0f,
            new Vector3(Mathf.Abs(xBounds.x - xBounds.y), Mathf.Abs(yBounds.x - yBounds.y), Mathf.Abs(zBounds.x - zBounds.y))
            );

        Gizmos.DrawSphere(grabSpot.position, grabDistance);
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (!manual_height) yTarget = transform.position.y;
        if (!m_rb) m_rb = GetComponent<Rigidbody>();

        foreach (Task _task in m_taskManager.allTasks)
        {
            _task.TaskFailed.AddListener(() => {
                AddRage(0.1f);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TryGrab();
        }
        else
        {
            TryRelease();
        }

        if (m_joint) anim.SetBool("IsOpen", false);
        else anim.SetBool("IsOpen", true);

        //AddRage(0.01f * Time.deltaTime);

        if (rageAmount < 0.5f) anim.SetBool("IsCalm", true);
        if (rageAmount >= 0.5f) anim.SetBool("IsCalm", false);
        if (rageAmount >= 1.0f)
        {
            anim.SetBool("Rage", true);
            axe.SetActive(true);
        }

        Vector3 desiredVel = Vector3.zero;

        float horiz = Input.GetAxis("Mouse X");
        float vert = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(1))
        {
            desiredVel += Vector3.down * m_moveSpeed * 4;
        }
        else
        {
            desiredVel += Vector3.ClampMagnitude((new Vector3(transform.position.x, yTarget, transform.position.z) - transform.position) * 10, m_moveSpeed * 2);
        }

        desiredVel += new Vector3(horiz, 0, vert) * 10 * m_moveSpeed;

        m_rb.velocity = desiredVel;

        if (transform.position.y < yBounds.x && m_rb.velocity.y <= 0)
        {
            m_rb.velocity = new Vector3(m_rb.velocity.x, 0, m_rb.velocity.z);
            transform.position = new Vector3(transform.position.x, yBounds.x, transform.position.z);
        }
        if (transform.position.y > yBounds.y && m_rb.velocity.y >= 0)
        {
            m_rb.velocity = new Vector3(m_rb.velocity.x, 0, m_rb.velocity.z);
            transform.position = new Vector3(transform.position.x, yBounds.y, transform.position.z);
        }
        
        if (transform.position.x < xBounds.x && m_rb.velocity.x <= 0)
        {
            m_rb.velocity = new Vector3(0, m_rb.velocity.y, m_rb.velocity.z);
            transform.position = new Vector3(xBounds.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xBounds.y && m_rb.velocity.x >= 0)
        {
            m_rb.velocity = new Vector3(0, m_rb.velocity.y, m_rb.velocity.z);
            transform.position = new Vector3(xBounds.y, transform.position.y, transform.position.z);
        }
       
        if (transform.position.z < zBounds.x && m_rb.velocity.z <= 0)
        {
            m_rb.velocity = new Vector3(m_rb.velocity.x, m_rb.velocity.y, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, zBounds.x);
        }
        if (transform.position.z > zBounds.y && m_rb.velocity.z >= 0)
        {
            m_rb.velocity = new Vector3(m_rb.velocity.x, m_rb.velocity.y, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, zBounds.y);
        }

        float ratio = (Vector3.Angle(transform.forward, Vector3.forward) + Vector3.Angle(transform.up, Vector3.up)) / (360.0f * 2.0f);

        m_rb.AddTorque(Vector3.up * Vector3.Dot(transform.forward, Vector3.left) * ratio * 3);
        m_rb.AddTorque(Vector3.right * Vector3.Dot(transform.forward, Vector3.up) * ratio * 3);
        m_rb.AddTorque(Vector3.forward * Vector3.Dot(transform.right, Vector3.down) * ratio * 3);
    }

    public void AddRage(float _amount)
    {
        rageAmount += _amount;

        rageAmount = Mathf.Clamp(rageAmount, 0, 1.0f);
    }

    public void TryRelease()
    {
        if (mouseDown)
        {
            if (m_joint)
            {
                HandCallback callback = m_joint.connectedBody.transform.GetComponent<HandCallback>();
                if (callback) callback.Release();
                Destroy(m_joint);
            }
            foreach (GameObject _part in fingers)
            {
                if (_part.GetComponent<Collider>()) _part.GetComponent<Collider>().enabled = true;
            }

            //GetComponent<Animation>().clip = GetComponent<Animation>().GetClip("Open");
            //GetComponent<Animation>().Play();
        }

        mouseDown = false;
    }

    public void TryGrab()
    {
        if (!m_joint)
        {
            

            bool didHit = false;

            Collider[] hits = Physics.OverlapSphere(grabSpot.position, grabDistance, grabbable);
            foreach (Collider _hit in hits)
            {
                Rigidbody hitBody = _hit.GetComponent<Rigidbody>();

                if (hitBody)
                {
                    m_joint = gameObject.AddComponent<ConfigurableJoint>();
                    m_joint.xMotion = ConfigurableJointMotion.Locked;
                    m_joint.yMotion = ConfigurableJointMotion.Locked;
                    m_joint.zMotion = ConfigurableJointMotion.Locked;
                    
                    if (hitBody.transform.root.name != "Joystick")
                    {
                        m_joint.angularXMotion = ConfigurableJointMotion.Locked;
                        m_joint.angularYMotion = ConfigurableJointMotion.Locked;
                        m_joint.angularZMotion = ConfigurableJointMotion.Locked;
                    }
                    
                    
                    m_joint.connectedBody = hitBody;

                    HandCallback callback = hitBody.GetComponent<HandCallback>();

                    if (callback) callback.Grabbed();

                    didHit = true;
                    break;
                }

            }

            if (didHit)
            {
                foreach (GameObject _part in fingers)
                {
                    if (_part.GetComponent<Collider>()) _part.GetComponent<Collider>().enabled = false;
                }
            }
        }

        if (!mouseDown)
        {
            //GetComponent<Animation>().clip = GetComponent<Animation>().GetClip("Close");
            //GetComponent<Animation>().Play();
        }
        mouseDown = true;
    }
}
