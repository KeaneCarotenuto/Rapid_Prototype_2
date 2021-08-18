using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public List<AudioClip> m_KeyPressSounds;
    public AudioSource m_source;
    public GameObject m_SoundPlayerPrefab;


    // Start is called before the first frame update

    public void PlaySound()
    {

        m_source.clip = m_KeyPressSounds[Random.Range(0, m_KeyPressSounds.Count)];
        m_source.Play();


    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
}
