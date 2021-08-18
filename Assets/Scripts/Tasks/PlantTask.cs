using UnityEngine;

public class PlantTask : Task
{
    public GameObject m_currentMeat;
    public GameObject MeatPrefab;

    public Transform meatPosition;
    public float spawnHeight = 10;

    [SerializeField] Animator anim;

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
    }

    private void LateUpdate()
    {
        if (!taskExists)
        {
            anim.SetBool("Idling", true);
            anim.SetBool("Waiting", false);
            anim.SetBool("Eating", false);
        }
    }

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
        meat.completed.AddListener(() => {
            CompleteTask();

            anim.SetBool("Idling", false);
            anim.SetBool("Waiting", false);
            anim.SetBool("Eating", true);
        });
        meat.failed.AddListener(() => { 
            FailTask(); 
        });

        taskExists = true;
    }
}
