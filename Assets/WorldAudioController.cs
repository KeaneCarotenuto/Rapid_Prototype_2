using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldAudioController : MonoBehaviour
{
    public AudioSource m_Ambience, m_Announcements;

    public AudioClip m_AmbientTrack;

    public List<AudioClip> m_AnnouncementList;

    public float m_AnnouncementInterval;
    public bool m_PlayAnnouncements;

    [Range(0, 100)]
    public int m_AnnouncementChance;
    float m_timer;

    public void PlayAnnouncement()
    {
        if (!m_Announcements.isPlaying)
        {
            m_Announcements.clip = m_AnnouncementList[Random.Range(0, m_AnnouncementList.Count)];
            m_Announcements.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //m_SoundEffects.clip = m_GameStart;

        //m_SoundEffects.Play();

        m_Ambience.clip = m_AmbientTrack;

        m_Ambience.Play();

        m_timer = m_AnnouncementInterval;
    }

    // Update is called once per frame
    void Update()
    {
        m_timer -= Time.deltaTime;
        if (m_timer < 0 && m_PlayAnnouncements)
        {
            if (Random.Range(0, 100) < m_AnnouncementChance)
            {
                PlayAnnouncement();
            }
            m_timer = m_AnnouncementInterval;
        }

    }
}
