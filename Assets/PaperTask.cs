using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperTask : MonoBehaviour
{
    bool m_crumpled = false;

    public GameObject m_stamp;

    [SerializeField] Mesh m_sphereMesh;

    [SerializeField] HandCallback m_handCallback;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (!m_crumpled && collision.transform == m_stamp.transform)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!m_handCallback.isGrabbed && other.name == "DetectZone")
        {
            Destroy(gameObject,1);
        }
    }
}
