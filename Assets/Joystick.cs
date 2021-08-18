using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Joystick : MonoBehaviour
{

    public Transform m_stick;
    public Vector3 m_rawinput;
    public UnityEvent OnJoystickUp, OnJoystickDown, OnJoystickLeft, OnJoystickRight;

    public bool UpLastFrame, DownLastFrame, LeftLastFrame, RightLastFrame, Up, Down, Left, Right;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<HandCallback>() && !GetComponent<HandCallback>().isGrabbed)
        {
            m_rawinput.x = 0;
            m_rawinput.y = 0;
            return;
        }

        Up = false;
        Down = false;
        Left = false;
        Right = false;

        if (m_stick.localRotation.x > 0.2f)
        {
            Up = true;
        }
        else if (m_stick.localRotation.x < -0.2f)
        {
            Down = true;
        }
        else if (m_stick.localRotation.z > 0.2f)
        {
            Left = true;
        }
        else if (m_stick.localRotation.z < -0.2f)
        {
            Right = true;
        }

        if (Up && !UpLastFrame)
        {
            OnJoystickUp.Invoke();
        }
        if (Down && !DownLastFrame)
        {
            OnJoystickDown.Invoke();
        }
        if (Left && !LeftLastFrame)
        {
            OnJoystickLeft.Invoke();
        }
        if (Right && !RightLastFrame)
        {
            OnJoystickRight.Invoke();
        }

        UpLastFrame = Up;
        DownLastFrame = Down;
        LeftLastFrame = Left;
        RightLastFrame = Right;

        m_rawinput.x = m_stick.localRotation.z * 30.0f;
        m_rawinput.y = m_stick.localRotation.x * -30.0f;
    }
}
