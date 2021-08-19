using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Squig : MonoBehaviour
{
    public UnityEvent OnSquish;
    public float m_SmashCooldown;
    float m_smashcooldowntimer = 0;
    public AudioClip m_SquigSmash;

    public Animator m_SquigAnim;
    public List<AudioClip> m_SquigRadiantSounds;

    public float m_AnimChangeInterval;
    [Range(0, 100)]
    public int m_FidgetChance;

    public AudioSource m_RadiantSource, m_EffectSource;

    public float m_RadiantSoundInterval;
    [Range(0, 100)]
    public int m_RadiantSoundChance;

    float m_timer, m_animtimer;

    [Header("Rage Stuff")]
    [SerializeField] GameObject m_hand;
    public float m_rageReduceCooldown;
    [SerializeField] float m_lastRageReduce;

    // Start is called before the first frame update
    void Start()
    {
        m_timer = m_RadiantSoundInterval;
        m_animtimer = m_AnimChangeInterval;

        m_lastRageReduce = -m_rageReduceCooldown;
    }

    // Update is called once per frame
    void Update()
    {

        m_timer -= Time.deltaTime;
        m_animtimer -= Time.deltaTime;
        m_smashcooldowntimer -= Time.deltaTime;
        if (m_timer <= 0)
        {
            m_timer = m_RadiantSoundInterval;
            if (Random.Range(0, 100) < m_RadiantSoundChance)
            {
                m_RadiantSource.clip = m_SquigRadiantSounds[Random.Range(0, m_SquigRadiantSounds.Count)];
                m_RadiantSource.Play();
            }

        }
        if (m_animtimer <= 0)
        {
            m_animtimer = m_AnimChangeInterval;
            if (Random.Range(0, 100) < m_FidgetChance)
            {
                m_SquigAnim.SetBool("Fidget", true);
            }
            else
            {
                m_SquigAnim.SetBool("Fidget", false);
            }
        }
    }

    void LateUpdate()
    {
        m_SquigAnim.SetBool("Squish", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (m_smashcooldowntimer <= 0)
        {
            m_smashcooldowntimer = m_SmashCooldown;
            OnSquish.Invoke();
            m_EffectSource.clip = m_SquigSmash;
            m_EffectSource.Play();
            m_SquigAnim.SetBool("Squish", true);

            if (other.transform.root.gameObject == m_hand && Time.time - m_lastRageReduce >= m_rageReduceCooldown)
            {
                m_lastRageReduce = Time.time;
                HandMovement hm = m_hand.GetComponent<HandMovement>();
                hm.AddRage(-0.1f);
            }
        }
    }


}
