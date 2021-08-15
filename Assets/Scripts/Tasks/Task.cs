using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Task : MonoBehaviour
{
    public enum TaskType
    {
        Paper,
        Phone,
        Plant
    }

    public UnityEvent TaskCompleted;
    public UnityEvent TaskFailed;

    public TaskType type;

    public bool taskExists;
    protected float startTime = 0;
    protected float taskTime = Mathf.Infinity;

    public struct QueueItem
    {
        public float m_taskTime;
    }

    public List<QueueItem> queue = new List<QueueItem>();

    void Update()
    {
        if (!taskExists && queue.Count > 0) StartTask(queue[0].m_taskTime);

        if (taskExists)
        {
            if (Time.time - startTime >= taskTime)
            {
                FailTask();
            }
        }
    }

    virtual public void QueueTask(float _time = Mathf.Infinity)
    {
        QueueItem _task;
        _task.m_taskTime = _time;

        queue.Add(_task);
    }

    virtual public void StartTask(float _time = Mathf.Infinity)
    {
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
        queue.RemoveAt(0);

        TaskCompleted.Invoke();

        Debug.Log("Completed");
    }

    virtual public void FailTask(bool requireTask = true)
    {
        if (requireTask && !taskExists) return;
        taskExists = false;
        queue.RemoveAt(0);

        TaskFailed.Invoke();

        Debug.Log("Failed");
    }
}
