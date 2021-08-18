using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    public UnityEvent addedTask;

    public List<Task> allTasks;

    public List<Task> currentTasks;

    public TextMeshProUGUI taskList;

    float lastTaskTime = 0;
    public float newTaskInterval = 10.0f;

    // Update is called once per frame
    void Update()
    {
        if (taskList) taskList.text = GetTaskListString();

        if (Time.time - lastTaskTime >= newTaskInterval)
        {
            lastTaskTime = Time.time;
            if (allTasks.Count > 0)
            {
                allTasks[Random.Range(0, allTasks.Count)].QueueTask(10);
                addedTask.Invoke();
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

                default:
                    break;
            }

            tasktListString += " (" + ((int)_task.GetRemainingTime()).ToString() + "s) x" + _task.queue.Count.ToString();
        }

        return tasktListString;
    }
}
