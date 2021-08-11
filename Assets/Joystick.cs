using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Joystick : MonoBehaviour
{

    public Transform m_stick;
    public UnityEvent OnJoystickUp, OnJoystickDown, OnJoystickLeft, OnJoystickRight;

    public bool UpLastFrame, DownLastFrame, LeftLastFrame, RightLastFrame, Up, Down, Left, Right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Up = false;
        Down = false;
        Left = false;
        Right = false;
        
        if(m_stick.localRotation.x > 0.3f)
        {
            Up = true;
        }
        else if(m_stick.localRotation.x < -0.3f)
        {
            Down = true;
        }
        else if(m_stick.localRotation.z > 0.3f)
        {
            Left = true;
        }
        else if(m_stick.localRotation.z < -0.3f)
        {
            Right = true;
        }

        if(Up && !UpLastFrame)
        {
            OnJoystickUp.Invoke();
        }
         if(Down && !DownLastFrame)
        {
            OnJoystickDown.Invoke();
        }
         if(Left && !LeftLastFrame)
        {
            OnJoystickLeft.Invoke();
        }
         if(Right && !RightLastFrame)
        {
            OnJoystickRight.Invoke();
        }

        UpLastFrame = Up;
        DownLastFrame = Down;
        LeftLastFrame = Left;
        RightLastFrame = Right;
    }
}
