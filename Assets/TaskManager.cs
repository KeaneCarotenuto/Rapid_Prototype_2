using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public List<TaskData> m_PossibleTasks;

    List<TaskData> m_ActiveTasks;

    public TMP_Text m_TaskBoardText;

    public float m_NewTaskInterval;

    public float m_NewTaskProbability;

    public int m_MaxConcurrentTasks;

    public Transform m_PneumaticTubeSpawn;

    public float m_ItemSpawnForceMult;

    public AudioClip m_TaskSuccessSound, m_TaskFailSound;
    public AudioSource m_Audio;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateBoard()
    {
        string boardtext = "Tasks:\n";
        foreach (var task in m_ActiveTasks)
        {

        }
    }

    void AddTask(TaskData _task)
    {
        TaskData newtask = Instantiate(_task);
        if (newtask.m_SpawnsItems)
        {
            foreach (var item in newtask.m_TaskItems)
            {
                GameObject newitem = Instantiate(item, m_PneumaticTubeSpawn.position, Quaternion.identity);
                newitem.GetComponent<Rigidbody>().AddForce(new Vector3(0, -m_ItemSpawnForceMult, 0), ForceMode.Impulse);
                newtask.m_SpawnedItems.Add(newitem);
            }
        }
        else
        {
            GameObject taskitem = GameObject.FindGameObjectWithTag(newtask.m_ItemTag);
        }
        newtask.m_TaskID = "T_" + Time.timeSinceLevelLoad;
        m_ActiveTasks.Add(newtask);

    }

    void RemoveTask(string _TaskID)
    {
        TaskData task = null;
        foreach (var t in m_ActiveTasks)
        {
            if (t.m_TaskID == _TaskID)
            {
                task = t;
            }
        }
        foreach (var item in task.m_SpawnedItems)
        {
            Destroy(item);
        }
        Destroy(task);
    }
    public void CompleteTask(string _taskID)
    {

    }

    public void FailTask(string _taskID)
    {

    }
}
