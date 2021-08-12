using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Paper : MonoBehaviour
{
    bool m_crumpled = false;
    bool m_stamped = false;

    public GameObject m_stamp;

    [SerializeField] Mesh m_sphereMesh;

    [SerializeField] HandCallback m_handCallback;

    public UnityEvent completed;
    public UnityEvent failed;

    // Start is called before the first frame update
    void Start()
    {
        //if (!m_stamp) m_stamp = GameObject.Find("Stamp");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Crumple()
    {
        if (m_crumpled) return;

        m_crumpled = true;
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        GetComponent<MeshFilter>().mesh = m_sphereMesh;
        GetComponent<MeshCollider>().sharedMesh = m_sphereMesh;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!m_crumpled && !m_stamped && collision.transform.name.Contains(m_stamp.name))
        {
            HandCallback callback = collision.gameObject.GetComponent<HandCallback>();
            if (callback && callback.isGrabbed)
            {
                GetComponent<MeshRenderer>().material.color = Color.red;
                m_stamped = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!m_handCallback.isGrabbed && other.name == "DetectZone")
        {
            Destroy(gameObject,1);
        }
    }

    private void OnDestroy()
    {
        if (m_crumpled && m_stamped)
        {
            completed.Invoke();
        }
        else
        {
            failed.Invoke();
        }
    }
}
