using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailInputProxy : MonoBehaviour
{
    public GameObject m_EmailObj;

    public void InputKey()
    {
        if (m_EmailObj.gameObject.activeSelf)
        {
            m_EmailObj.GetComponent<EmailInput>().InputKey();
        }
    }
}
