using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhoneTask : Task
{
    public List<GameObject> m_buttons;
    public AudioSource m_source;
    public AudioClip m_ringclip, m_pickupclip, m_dropclip;
    public Material red;
    public Material green;
    public Material white;

    public override void StartTask(float _time = Mathf.Infinity)
    {
        if (taskExists) return;
        startTime = Time.time;
        taskTime = _time;

        foreach (GameObject _button in m_buttons)
        {
            _button.GetComponent<Renderer>().material.color = Color.green;
        }
        m_source.clip = m_ringclip;
        m_source.loop = true;
        m_source.Play();
        //m_light.GetComponent<Renderer>().material = green;

        taskExists = true;
    }

    public void PickedUp()
    {
        if (taskExists)
        {
            foreach (GameObject _button in m_buttons)
            {
                _button.GetComponent<Renderer>().material.color = Color.red;
            }
            m_source.loop = false;
            m_source.clip = m_pickupclip;
            m_source.Play();
        }
    }

    public void HungUp()
    {
        foreach (GameObject _button in m_buttons)
        {
            _button.GetComponent<Renderer>().material.color = Color.white;
        }
        m_source.loop = false;
        m_source.clip = m_dropclip;
        m_source.Play();
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
