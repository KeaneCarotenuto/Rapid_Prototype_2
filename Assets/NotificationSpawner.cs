using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationSpawner : MonoBehaviour
{
    public GameObject m_Prefab;
    public Transform m_SpawnedElementParent;

    [SerializeField] Keyboard m_keyboard;

    public void Spawn(string _message)
    {
        GameObject popup = Instantiate(m_Prefab, m_SpawnedElementParent);
        popup.GetComponent<Notificationbox>().Message = _message;
        popup.GetComponent<Notificationbox>().m_keyboard = m_keyboard;
    }

}
