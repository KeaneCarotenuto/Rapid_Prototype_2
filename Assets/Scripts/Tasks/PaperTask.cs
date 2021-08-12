using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PaperTask : MonoBehaviour
{
    public UnityEvent TaskCompleted;
    public UnityEvent TaskFailed;

    public GameObject m_currentPaper;
    public GameObject PaperPrefab;

    public bool taskExists;

    public Transform paperPosition;
    public float spawnHeight = 10;

    // Start is called before the first frame update
    void Start()
    {
        //paperPosition = m_currentPaper.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!taskExists && !m_currentPaper) StartTask();
    }

    public void StartTask()
    {
        if (taskExists) return;
        if (!m_currentPaper) m_currentPaper = Instantiate(PaperPrefab, paperPosition.position +  new Vector3(0, spawnHeight, 0), Quaternion.identity, transform);
        Paper paper = m_currentPaper.GetComponent<Paper>();
        paper.completed.AddListener(CompleteTask);
        paper.failed.AddListener(FailTask);

        taskExists = true;
    }

    public void CompleteTask()
    {
        if (!taskExists) return;
        taskExists = false;
        TaskCompleted.Invoke();
        Debug.Log("Completed");
    }

    public void FailTask()
    {
        if (!taskExists) return;
        taskExists = false;
        TaskFailed.Invoke();
        Debug.Log("Failed");
    }
}
