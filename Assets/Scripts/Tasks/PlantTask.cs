using UnityEngine;

public class PlantTask : Task
{
    public GameObject m_currentMeat;
    public GameObject MeatPrefab;

    public Transform meatPosition;
    public float spawnHeight = 10;

    [SerializeField] Animator anim;

    public override void StartTask(float _time = Mathf.Infinity)
    {
        if (taskExists) return;
        startTime = Time.time;
        taskTime = _time;

        anim.SetBool("Idling", false);
        anim.SetBool("Waiting", true);
        anim.SetBool("Eating", false);

        if (!m_currentMeat) m_currentMeat = Instantiate(MeatPrefab, meatPosition.position + new Vector3(0, spawnHeight, 0), Quaternion.identity, transform);
        Meat meat = m_currentMeat.GetComponent<Meat>();
        meat.completed.AddListener(CompleteTask);
        meat.failed.AddListener(() => { 
            FailTask(); 
        });
        TaskFailed.AddListener(() => {
            if (m_currentMeat)
            {
                Destroy(m_currentMeat);
                m_currentMeat = null;
            }

            anim.SetBool("Idling", false);
            anim.SetBool("Waiting", true);
            anim.SetBool("Eating", false);
        });

        taskExists = true;
    }
}
