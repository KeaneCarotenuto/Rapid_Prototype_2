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

    public override void StartTask(float _time = Mathf.Infinity)
    {
        if (taskExists) return;
        startTime = Time.time;
        taskTime = _time;

        m_light.GetComponent<Renderer>().material = green;
        
        taskExists = true;
    }

    public void PickedUp()
    {
        if (taskExists)
        {
            m_light.GetComponent<Renderer>().material = red;
        }
    }

    public void HungUp()
    {
        m_light.GetComponent<Renderer>().material = white;
        if (taskExists)
        {
            CompleteTask();
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
}
