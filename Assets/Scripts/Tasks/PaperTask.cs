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

    void Update()
    {
        if (!taskExists && queue.Count > 0) StartTask(queue[0].m_taskTime);

        if (taskExists)
        {
            if (Time.time - startTime >= taskTime)
            {
                FailTask();
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            QueueTask(30);
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
        TaskFailed.AddListener(() => { 
            if (m_currentPaper)
            {
                Destroy(m_currentPaper);
                m_currentPaper = null;
            }
        });

        taskExists = true;
    }
}
