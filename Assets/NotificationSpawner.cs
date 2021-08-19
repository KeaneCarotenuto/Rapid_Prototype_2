using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationSpawner : MonoBehaviour
{
    public GameObject m_Prefab;
    public Transform m_SpawnedElementParent;

    public void Spawn(string _message)
    {
        Instantiate(m_Prefab, m_SpawnedElementParent).GetComponent<Notificationbox>().Message = _message;
    }

}
