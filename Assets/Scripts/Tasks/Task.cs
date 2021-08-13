using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Task : MonoBehaviour
{
    string id;

    public UnityEvent TaskCompleted;
    public UnityEvent TaskFailed;
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
        TaskCompleted.Invoke();

        Debug.Log("Completed");
    }

    virtual public void FailTask(bool requireTask = true)
    {
        if (requireTask && !taskExists) return;
        taskExists = false;
        TaskFailed.Invoke();

        Debug.Log("Failed");
    }
}
