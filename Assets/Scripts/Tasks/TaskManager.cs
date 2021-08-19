using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    public UnityEvent OnTaskAdd;

    public NotificationSpawner m_Spawner;
    public List<Task> allTasks;

    public List<Task> currentTasks;

    public TextMeshProUGUI taskList;

    float lastTaskTime = 0;
    public float newTaskInterval = 10.0f;
    [Range(1, 100)]
    public int newTaskProbability;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //taskList.text = GetTaskListString();

        if (Time.time - lastTaskTime >= newTaskInterval)
        {
            lastTaskTime = Time.time;
            if (Random.Range(0, 100) <= newTaskProbability)
            {
                OnTaskAdd.Invoke();

                Task chosen = allTasks[Random.Range(0, allTasks.Count)];
                chosen.QueueTask(Random.Range(15, 22));
                switch (chosen.type)
                {
                    case Task.TaskType.Email:
                        m_Spawner.Spawn("Send an Email!");
                        break;
                    case Task.TaskType.Phone:
                        m_Spawner.Spawn("Answer the Phone!");
                        break;
                    case Task.TaskType.Plant:
                        m_Spawner.Spawn("Feed the Plant!");
                        break;
                    case Task.TaskType.Paper:
                        m_Spawner.Spawn("Stamp the Paper!");
                        break;

                    default:
                        break;
                }

            }

        }
    }

    private string GetTaskListString()
    {
        string tasktListString = "Tasks:";

        List<Task> tempList = allTasks;
        tempList.Sort((p1, p2) => p1.GetRemainingTime().CompareTo(p2.GetRemainingTime()));

        foreach (Task _task in tempList)
        {
            if (_task.queue.Count <= 0) continue;

            switch (_task.type)
            {
                case Task.TaskType.Paper:
                    tasktListString += "\nPaper";
                    break;

                case Task.TaskType.Phone:
                    tasktListString += "\nPhone";
                    break;

                case Task.TaskType.Plant:
                    tasktListString += "\nPlant";
                    break;

                case Task.TaskType.Email:
                    tasktListString += "\nEmail";
                    break;

                default:
                    break;
            }

            tasktListString += " (" + ((int)_task.GetRemainingTime()).ToString() + "s) x" + _task.queue.Count.ToString();
        }

        return tasktListString;
    }

    public List<string> GetTaskListStrings()
    {


        List<Task> tempList = allTasks;
        List<string> tasklist = new List<string>();
        tempList.Sort((p1, p2) => p1.GetRemainingTime().CompareTo(p2.GetRemainingTime()));

        foreach (Task _task in tempList)
        {
            if (_task.queue.Count <= 0) continue;
            string newstr = "";

            switch (_task.type)
            {
                case Task.TaskType.Paper:
                    newstr += "Paper";
                    break;

                case Task.TaskType.Phone:
                    newstr += "Phone";
                    break;

                case Task.TaskType.Plant:
                    newstr += "Plant";
                    break;
                case Task.TaskType.Email:
                    newstr += "Email";
                    break;

                default:
                    break;
            }

            newstr += " (" + ((int)_task.GetRemainingTime()).ToString() + "s) x" + _task.queue.Count.ToString();
            tasklist.Add(newstr);
        }

        return tasklist;
    }
}
