using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailInputProxy : MonoBehaviour
{
    public GameObject m_EmailObj;

    public void InputKey(int times = 1)
    {
        if (m_EmailObj.gameObject.activeSelf)
        {
            m_EmailObj.GetComponent<EmailInput>().InputKey(times);
        }
    }
}
