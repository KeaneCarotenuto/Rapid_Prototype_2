using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCUIcontroller : MonoBehaviour
{
    public Texture m_DesktopEmailSelected, m_DesktopTasksSelected, m_TasksDoneSelected, m_TasksCloseSelected, EmailTextboxSelected, EmailSendSelected, EmailCloseSelected;
    public RawImage m_Monitor;
    Texture m_currTexture;
    public enum ProgramState
    {
        DESKTOP,
        EMAIL,
        TASK,
    }

    public enum DesktopSelection
    {
        TASKS,
        EMAIL,
    }

    public enum TasksSelection
    {
        DONE,
        CLOSE,
    }

    public enum EmailSelection
    {
        TEXTBOX,
        SEND,
        EXIT,
    }

    public ProgramState m_currentState;
    public DesktopSelection m_dtopSelection;
    public TasksSelection m_taskSelection;
    public EmailSelection m_emailSelection;

    public void Navigate(int _direction)
    {
        switch(m_currentState)
        {
            case ProgramState.DESKTOP:
                switch (m_dtopSelection)
                {
                    case DesktopSelection.TASKS:
                        if (_direction == 0)
                        {
                            m_dtopSelection = DesktopSelection.EMAIL;
                            m_currTexture = m_DesktopEmailSelected;
                        }
                        break;
                    case DesktopSelection.EMAIL:
                        if (_direction == 2)
                        {
                            m_dtopSelection = DesktopSelection.TASKS;
                            m_currTexture = m_DesktopTasksSelected;
                        }
                        break;
                    default:
                        break;
                }
                break;
            case ProgramState.EMAIL:
                switch (m_emailSelection)
                {
                    case EmailSelection.TEXTBOX:
                        if (_direction == 0)
                        {
                            m_emailSelection = EmailSelection.SEND;
                        }
                        else if (_direction == 0)
                        {
                            m_emailSelection = EmailSelection.EXIT;
                        }
                        break;
                    case EmailSelection.SEND:
                        break;
                    case EmailSelection.EXIT:
                        break;
                    default:
                        break;
                }
                break;
            case ProgramState.TASK:
            break;
            default:
            break;
        }
    }

    public void EnterSelected()
    {
        switch (m_currentState)
        {
            case ProgramState.DESKTOP:
                switch (m_dtopSelection)
                {
                    case DesktopSelection.TASKS:
                        m_currentState = ProgramState.TASK;
                        m_currTexture = m_TasksCloseSelected;
                        m_taskSelection = TasksSelection.CLOSE;
                        break;
                    case DesktopSelection.EMAIL:
                        m_currentState = ProgramState.EMAIL;
                        m_currTexture = EmailCloseSelected; 
                        m_emailSelection = EmailSelection.EXIT;
                        break;
                    default:
                        break;
                }
                break;
            case ProgramState.EMAIL:
                break;
            case ProgramState.TASK:
                break;
            default:
                break;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        m_currTexture = m_DesktopEmailSelected;
    }

    // Update is called once per frame
    void Update()
    {
        m_Monitor.texture = m_currTexture;
    }
}
