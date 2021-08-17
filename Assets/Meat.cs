using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Meat : MonoBehaviour
{
    [SerializeField] HandCallback m_handCallback;

    public UnityEvent completed;
    public UnityEvent failed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Plate")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_handCallback.isGrabbed && other.name == "MeatZone")
        {
            Destroy(gameObject, 1);
        }
    }

    private void OnDestroy()
    {
        completed.Invoke();
    }

}
