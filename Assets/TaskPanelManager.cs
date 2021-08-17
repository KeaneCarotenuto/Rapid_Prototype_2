using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPanelManager : MonoBehaviour
{
    public TaskManager m_manager;
    public GameObject m_taskpanelprefab;
    void Awake()
    {
        int i = 0;

        GameObject[] allChildren = new GameObject[transform.childCount];

        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }


        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }

        //foreach (var task in m_manager.m_ActiveTasks)
        //{
        //    Instantiate(m_taskpanelprefab, this.transform).GetComponent<TaskPanel>().Task = task;
        //}
    }
}
