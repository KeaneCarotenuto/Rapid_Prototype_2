using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Paper : MonoBehaviour
{
    public AudioSource m_AudioSource;
    public AudioClip m_crumplesound, m_stampsound;
    bool m_crumpled = false;
    bool m_stamped = false;

    public GameObject m_stamp;

    [SerializeField] Mesh m_crumpledMesh;

    [SerializeField] HandCallback m_handCallback;

    public UnityEvent completed;
    public UnityEvent failed;

    public void Crumple()
    {
        if (m_crumpled) return;

        m_crumpled = true;
        m_AudioSource.clip = m_crumplesound;
        m_AudioSource.Play();
        GetComponent<MeshFilter>().mesh = m_crumpledMesh;
        GetComponent<MeshCollider>().sharedMesh = m_crumpledMesh;
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
                m_AudioSource.clip = m_stampsound;
                m_AudioSource.Play();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PaperZone")
        {
            Destroy(gameObject);
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
