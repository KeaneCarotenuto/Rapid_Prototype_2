using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    public float m_moveSpeed = 1.0f;

    float targetHeight = 1.25f;

    public Rigidbody m_rb;

    bool mouseDown = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!m_rb) m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredVel = Vector3.zero;

        if (Input.GetKey(KeyCode.Space))
        {
            desiredVel += Vector3.down * m_moveSpeed * 2;
        }
        else
        {
            desiredVel += Vector3.ClampMagnitude((new Vector3(transform.position.x, targetHeight, transform.position.z) - transform.position) * 10, m_moveSpeed);
        }

        if (Input.GetKey(KeyCode.W))
        {
            desiredVel += Vector3.forward  * m_moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            desiredVel += Vector3.back * m_moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            desiredVel += Vector3.left * m_moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            desiredVel += Vector3.right * m_moveSpeed;
        }

        m_rb.velocity = desiredVel;


        float ratio = (Vector3.Angle(transform.forward, Vector3.forward) + Vector3.Angle(transform.up, Vector3.up)) / (360.0f * 2.0f);

        m_rb.AddTorque(Vector3.up * Vector3.Dot(transform.forward, Vector3.left) * ratio);
        m_rb.AddTorque(Vector3.right * Vector3.Dot(transform.forward, Vector3.up) * ratio);
        m_rb.AddTorque(Vector3.forward * Vector3.Dot(transform.right, Vector3.down) * ratio);

        if (Input.GetMouseButton(0))
        {
            if (!mouseDown)
            {
                GetComponent<Animation>().clip = GetComponent<Animation>().GetClip("Close");
                GetComponent<Animation>().Play();
            }
            mouseDown = true;
        }
        else
        {
            if (mouseDown)
            {
                GetComponent<Animation>().clip = GetComponent<Animation>().GetClip("Open");
                GetComponent<Animation>().Play();
            }

            mouseDown = false;
        }

    }
}
