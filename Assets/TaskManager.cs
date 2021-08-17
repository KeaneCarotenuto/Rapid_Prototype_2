using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public List<TaskData> m_PossibleTasks;

    public List<TaskData> m_ActiveTasks;

    public TMP_Text m_TaskBoardText;

    public float m_NewTaskInterval;

    float m_timer;

    public float m_NewTaskProbability;

    public int m_MaxConcurrentTasks;


    public AudioClip m_TaskSuccessSound, m_TaskFailSound, m_NewTaskSound;
    public AudioSource m_Audio;

    // Start is called before the first frame update
    void Start()
    {
        m_timer = m_NewTaskInterval;
    }

    // Update is called once per frame
    void Update()
    {
        m_timer -= Time.deltaTime;
        if (m_timer <= 0)
        {
            m_timer = m_NewTaskInterval;
            if (Random.Range(0, 100) <= m_NewTaskProbability && m_ActiveTasks.Count < m_MaxConcurrentTasks)
            {
                AddTask(m_PossibleTasks[Random.Range(0, m_PossibleTasks.Count)]);
                m_Audio.clip = m_NewTaskSound;
                m_Audio.Play();
            }
        }
        UpdateBoard();
    }

    void UpdateBoard()
    {
        string boardtext = "Tasks:\n";
        foreach (TaskData task in m_ActiveTasks)
        {
            boardtext += task.m_TaskDesc + " " + task.m_Task.GetRemainingTime() + "\n";
        }
        m_TaskBoardText.SetText(boardtext);
    }

    void AddTask(TaskData _task)
    {
        TaskData newtask = Instantiate(_task);

        GameObject taskitem = GameObject.FindGameObjectWithTag(newtask.m_ItemTag);
        Task taskscript = taskitem.GetComponentInChildren<Task>();
        newtask.m_Task = taskscript;


        newtask.m_TaskID = "T_" + Time.timeSinceLevelLoad;
        newtask.m_Task.SetId(newtask.m_TaskID);
        //newtask.m_Task.StartTask(newtask.m_TaskID, newtask.m_TaskDuration);
        m_ActiveTasks.Add(newtask);

    }

    void RemoveTask(string _TaskID)
    {
        TaskData task = null;
        foreach (var t in m_ActiveTasks)
        {
            if (t.m_TaskID == _TaskID)
            {
                task = t;
            }
        }

        Destroy(task);
    }
    public void CompleteTask(string _taskID)
    {
        RemoveTask(_taskID);
        m_Audio.clip = m_TaskSuccessSound;
        m_Audio.Play();
    }

    public void FailTask(string _taskID)
    {
        RemoveTask(_taskID);
        m_Audio.clip = m_TaskSuccessSound;
        m_Audio.Play();
    }
}
