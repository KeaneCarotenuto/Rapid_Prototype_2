using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Joystick : MonoBehaviour
{
    public float m_sensitivity;
    public float m_deadzone;
    public Transform m_stick;
    public Vector3 m_rawinput;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(m_stick.localRotation.z) < m_deadzone)
        {
            m_rawinput.x = m_stick.localRotation.z * m_sensitivity;
        }
        else
        {
            m_rawinput.x = 0.0f;
        }
        if (Mathf.Abs(m_stick.localRotation.x) < m_deadzone)
        {
            m_rawinput.y = m_stick.localRotation.x * -m_sensitivity;
        }
        else
        {
            m_rawinput.y = 0.0f;
        }
    }
}
