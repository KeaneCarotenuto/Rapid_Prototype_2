using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Keyboard : MonoBehaviour
{
    public List<AudioClip> m_KeyPressSounds;
    public UnityEvent OnPress;
    public float m_ActuationDepth;
    public List<Transform> m_Buttons;
    public List<float> m_startYvalues;
    public bool[] m_pressedLastFrame;
    // Start is called before the first frame update
    void Start()
    {
        m_pressedLastFrame = new bool[100];
        foreach (var button in m_Buttons)
        {
            m_startYvalues.Add(button.localPosition.y);
        }
    }

    // Update is called once per frame
    void Update()
    {

        for (var i = 0; i < m_Buttons.Count; i++)
        {
            if (m_Buttons[i].localPosition.y < m_startYvalues[i] - m_ActuationDepth && !m_pressedLastFrame[i])
            {
                OnPress.Invoke();
                if (!m_Buttons[i].gameObject.GetComponent<AudioSource>())
                {
                    m_Buttons[i].gameObject.AddComponent<AudioSource>();
                }
                m_Buttons[i].GetComponent<AudioSource>().clip = m_KeyPressSounds[Random.Range(0, m_KeyPressSounds.Count)];

                m_Buttons[i].GetComponent<AudioSource>().Play();

            }
            m_pressedLastFrame[i] = m_Buttons[i].localPosition.y < m_startYvalues[i] - m_ActuationDepth;
        }




    }
}
