using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public List<AudioClip> m_KeyPressSounds;
    public GameObject m_SoundPlayerPrefab;

    Queue<AudioSource> m_players;
    // Start is called before the first frame update

    public void PlaySound()
    {
        AudioSource newsound = Instantiate(m_SoundPlayerPrefab, this.transform).GetComponent<AudioSource>();
        newsound.clip = m_KeyPressSounds[Random.Range(0, m_KeyPressSounds.Count)];
        newsound.Play();
        m_players.Enqueue(newsound);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!m_players.Peek().isPlaying)
        {
            Destroy(m_players.Dequeue().gameObject);
        }
    }
}
