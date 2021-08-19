using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Meat : MonoBehaviour
{
    [SerializeField] HandCallback m_handCallback;
    public AudioSource m_source;
    public AudioClip m_slapclip;
    public UnityEvent completed;
    public UnityEvent failed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "item_platform")
        {

            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            m_source.clip = m_slapclip;
            m_source.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_handCallback.isGrabbed && other.name == "MeatZone")
        {
            completed.Invoke();
            Destroy(gameObject);
        }
    }

}
