using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskPanel : MonoBehaviour
{
    public TMP_Text TaskText;
    public TMP_Text TimeText;

    public TaskData Task;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Task == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            TaskText.text = Task.m_TaskDesc;
            TimeText.text = Task.m_Task.GetRemainingTime().ToString();
        }
    }
}
