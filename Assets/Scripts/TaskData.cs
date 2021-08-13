using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskData : ScriptableObject
{
    public string m_TaskID;
    public string m_TaskDesc;

    public float m_TaskDuration;

    public bool m_SpawnsItems;

    public string m_ItemTag;

    public List<GameObject> m_TaskItems;

    public List<GameObject> m_SpawnedItems;
}
