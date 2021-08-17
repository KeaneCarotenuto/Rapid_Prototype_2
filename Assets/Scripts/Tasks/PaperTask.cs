using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PaperTask : Task
{
    public GameObject m_currentPaper;
    public GameObject PaperPrefab;

    public Transform paperPosition;
    public float spawnHeight = 10;

    // Update is called once per frame
    void Update()
    {
        if (!taskExists && !m_currentPaper) StartTask(10.0f);

        if (taskExists)
        {
            if (Time.time - startTime >= taskTime)
            {
                FailTask();
            }
        }
    }

    public override void StartTask(float _time = Mathf.Infinity)
    {
        if (taskExists) return;
        startTime = Time.time;
        taskTime = _time;

        if (!m_currentPaper) m_currentPaper = Instantiate(PaperPrefab, paperPosition.position + new Vector3(0, spawnHeight, 0), Quaternion.identity, transform);
        Paper paper = m_currentPaper.GetComponent<Paper>();
        paper.completed.AddListener(CompleteTask);
        paper.failed.AddListener(() => { FailTask(); });

        taskExists = true;
    }

    public override void CompleteTask()
    {
        if (!taskExists) return;
        taskExists = false;
        TaskCompleted.Invoke(id);
        Debug.Log("Completed");
    }

    public override void FailTask(bool requireTask = true)
    {
        if (requireTask && !taskExists) return;
        taskExists = false;
        TaskFailed.Invoke(id);
        Debug.Log("Failed");

        if (m_currentPaper)
        {
            Destroy(m_currentPaper);
            m_currentPaper = null;
        }
    }
}
