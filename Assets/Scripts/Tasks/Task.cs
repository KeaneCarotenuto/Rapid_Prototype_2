using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskEvent : UnityEvent<string>
{

}

public struct TaskStruct
{
    float Timer;
    string ID;

    public TaskStruct(float _time, string _id)
    {
        Timer = _time;
        ID = _id;
    }
}

public class Task : MonoBehaviour
{
    Queue<TaskStruct> m_taskqueue;
    void Start()
    {
        TaskManager mgr = FindObjectOfType<TaskManager>();
        TaskCompleted.AddListener(mgr.CompleteTask);
        TaskFailed.AddListener(mgr.FailTask);
    }
    protected string id;

    public TaskEvent TaskCompleted;
    public TaskEvent TaskFailed;
    public bool taskExists;
    protected float startTime = 0;
    protected float taskTime = Mathf.Infinity;

    virtual public void SetId(string _id)
    {
        id = _id;
    }

    virtual public string GetId()
    {
        return id;
    }

    virtual public void StartTask(string _ID, float _time = Mathf.Infinity)
    {
        m_taskqueue.Enqueue(new TaskStruct)
        if (taskExists) return;
        startTime = Time.time;
        taskTime = _time;
        taskExists = true;

        Debug.Log("Started");
    }

    virtual public float GetRemainingTime()
    {
        if (!taskExists) return 0;

        return taskTime - (Time.time - startTime);
    }

    virtual public void CompleteTask()
    {
        if (!taskExists) return;
        taskExists = false;
        TaskCompleted.Invoke(id);

        Debug.Log("Completed");
    }

    virtual public void FailTask(bool requireTask = true)
    {
        if (requireTask && !taskExists) return;
        taskExists = false;
        TaskFailed.Invoke(id);

        Debug.Log("Failed");
    }
}
