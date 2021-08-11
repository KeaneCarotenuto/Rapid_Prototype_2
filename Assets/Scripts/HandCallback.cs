using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandCallback : MonoBehaviour
{
    public GameObject m_hand;

    public UnityEvent HandHit;
    public UnityEvent HandGrab;

    // Start is called before the first frame update
    void Start()
    {
        if (!m_hand) m_hand = GameObject.Find("Hand");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grabbed()
    {
        HandGrab.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root == m_hand.transform) HandHit.Invoke();
    }
}
