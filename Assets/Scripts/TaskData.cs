using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "TaskData", menuName = "TaskData", order = 0)]
public class TaskData : ScriptableObject
{
    public string m_TaskID;
    public string m_TaskDesc;

    public float m_TaskDuration;

    public string m_ItemTag;

    public Task m_Task;
}
