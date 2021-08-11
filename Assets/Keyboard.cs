using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Keyboard : MonoBehaviour
{
    public bool FirstButton, SecondButton, ThirdButton, FirstButtonLastFrame, SecondButtonLastFrame, ThirdButtonLastFrame;
    public Transform FirstTransform, SecondTransform, ThirdTransform;
    public UnityEvent OnFirstPress, OnSecondPress, OnThirdPress;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FirstButton = false;
        SecondButton = false;
        ThirdButton = false;
        

        if (FirstTransform.localPosition.y <= 0.03f)
        {
            FirstButton = true;
        }
        else if (SecondTransform.localPosition.y <= 0.03f)
        {
            SecondButton = true;
        }
        else if (ThirdTransform.localPosition.y <= 0.03f)
        {
            ThirdButton = true;
        }
        

        if (FirstButton && !FirstButtonLastFrame)
        {
            OnFirstPress.Invoke();
        }
        if (SecondButton && !SecondButtonLastFrame)
        {
            OnSecondPress.Invoke();
        }
        if (ThirdButton && !ThirdButtonLastFrame)
        {
            OnThirdPress.Invoke();
        }
       

        FirstButtonLastFrame = FirstButton;
        SecondButtonLastFrame = SecondButton;
        ThirdButtonLastFrame = ThirdButton;
        
    }
}
