using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhoneTask : MonoBehaviour
{
    public UnityEvent TaskCompleted;
    public UnityEvent TaskFailed;

    public bool taskExists;

    [SerializeField] bool onCall = false;

    public GameObject m_light;

    public Material red;
    public Material green;
    public Material white;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (!taskExists) StartTask();
    }

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
            onCall = true;
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

            onCall = false;
            taskExists = false;
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

    public void CompleteTask()
    {
        if (!taskExists) return;
        taskExists = false;
        TaskCompleted.Invoke();
        Debug.Log("Completed");
    }

    public void FailTask(bool requireTask = true)
    {
        if (requireTask && !taskExists) return;
        taskExists = false;
        TaskFailed.Invoke();
        Debug.Log("Failed");
    }
}
