using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhoneTask : Task
{
    public GameObject m_light;

    public Material red;
    public Material green;
    public Material white;

    public void StartTask()
    {
        if (taskExists) return;

        m_light.GetComponent<Renderer>().material = green;
        taskExists = true;
    }

    public void PickedUp()
    {
        if (taskExists)
        {
            m_light.GetComponent<Renderer>().material = red;
        }
        else
        {
            FailTask(false);
        }
    }

    public void HungUp()
    {
        m_light.GetComponent<Renderer>().material = white;
        if (taskExists)
        {
            CompleteTask();
        }
        else
        {
            FailTask();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "PhoneZone")
        {
            PickedUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PhoneZone")
        {
            HungUp();
        }
    }

    public override void CompleteTask()
    {
        if (!taskExists) return;
        taskExists = false;
        TaskCompleted.Invoke();
        Debug.Log("Completed");
    }

    public override void FailTask(bool requireTask = true)
    {
        if (requireTask && !taskExists) return;
        taskExists = false;
        TaskFailed.Invoke();
        Debug.Log("Failed");
    }
}
