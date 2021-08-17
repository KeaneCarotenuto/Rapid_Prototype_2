using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class EmailInput : MonoBehaviour
{
    public UnityEvent OnSuccessfulSend;
    public List<string> m_Recipients, m_CCs, m_Subjects, m_Contents;

    string m_chosenRecipient, m_chosenCC, m_chosenSubject, m_chosenContent;

    public TMP_Text m_recText, m_ccText, m_subText, m_conText;

    int m_reccount, m_cccount, m_subcount, m_concount;

    public bool m_FinishedWriting = false;
    // Start is called before the first frame update

    void Start()
    {
        m_chosenRecipient = m_Recipients[Random.Range(0, m_Recipients.Count)];
        m_chosenCC = m_CCs[Random.Range(0, m_CCs.Count)];
        m_chosenSubject = m_Subjects[Random.Range(0, m_Subjects.Count)];
        m_chosenContent = m_Contents[Random.Range(0, m_Contents.Count)];

        m_recText.text = "";
        m_ccText.text = "";
        m_subText.text = "";
        m_conText.text = "";

        m_reccount = 0;
        m_cccount = 0;
        m_subcount = 0;
        m_concount = 0;

        m_FinishedWriting = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InputKey()
    {
        if (m_FinishedWriting) return;

        if (m_recText.text.Length < m_chosenRecipient.Length)
        {
            m_recText.text += m_chosenRecipient[m_reccount++];
        }
        else if (m_ccText.text.Length < m_chosenCC.Length)
        {
            m_ccText.text += m_chosenCC[m_cccount++];
        }
        else if (m_subText.text.Length < m_chosenSubject.Length)
        {
            m_subText.text += m_chosenSubject[m_subcount++];
        }
        else if (m_conText.text.Length < m_chosenContent.Length)
        {
            m_conText.text += m_chosenContent[m_concount++];
        }
        else
        {
            m_conText.text += "\n\nYours Gruntily,\nOrc";
            m_FinishedWriting = true;
        }
    }

    public void SendEmail()
    {
        if (m_FinishedWriting)
        {
            m_recText.text = "";
            m_ccText.text = "";
            m_subText.text = "";
            m_conText.text = "";
            OnSuccessfulSend.Invoke();
        }
    }
}
