using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreens_Script : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_clock_text;

    [SerializeField] GameObject m_hand;

    [SerializeField] GameObject m_winScreen;
    [SerializeField] Image m_survivedImage;
    [SerializeField] GameObject m_loseScreen;
    [SerializeField] Image m_firedImage;
    [SerializeField] Image m_black;

    public float TimeToSurvive = 120;
    float startTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (TimeToSurvive <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Animator anim = m_hand.GetComponent<HandMovement>().anim;
            if (anim)
            {
                anim.SetBool("FlipOff", true);
            }

            TimeToSurvive = 0;

            m_winScreen.SetActive(true);

            m_survivedImage.color += new Color(0, 0, 0, 1.0f) * Time.deltaTime;
        }
        else if (m_hand.GetComponent<HandMovement>().GetRage() >= 1) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            m_loseScreen.SetActive(true);

            m_firedImage.color += new Color(0, 0, 0, 10.0f) * Time.deltaTime;
            m_black.color += new Color(0, 0, 0, 0.22f) * Time.deltaTime;
        }
        else
        {
            TimeToSurvive -= Time.deltaTime;
            m_clock_text.text = "Time Left: " + Mathf.Floor(TimeToSurvive / 60.0f).ToString() + ":" + Mathf.Floor((int)TimeToSurvive % 60).ToString();
        }
    }
}
