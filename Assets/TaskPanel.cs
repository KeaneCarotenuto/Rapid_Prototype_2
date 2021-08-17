using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskPanel : MonoBehaviour
{
    public TMP_Text TaskText;
    public TMP_Text TimeText;

    public Task Task;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Task == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            switch (Task.type)
            {
                case Task.TaskType.Paper:
                    TaskText.text = "Stamp the paper!";
                    break;
                case Task.TaskType.Phone:
                    TaskText.text = "Answer the Phone!";
                    break;
                case Task.TaskType.Plant:
                    TaskText.text = "Feed the Plant!";
                    break;
                default:
                    break;
            }
            TimeText.text = Task.GetRemainingTime().ToString();
        }
    }
}
