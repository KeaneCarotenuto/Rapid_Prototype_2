using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour
{
    public Joystick m_joystick;

    PointerEventData m_PointerEventData;
    [SerializeField] GraphicRaycaster m_Raycaster;
    [SerializeField] EventSystem m_EventSystem;
    [SerializeField] RectTransform canvasRect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += m_joystick.m_rawinput * Time.deltaTime * 100;
        Vector3 tmpPos = transform.localPosition;
        tmpPos.x = Mathf.Clamp(tmpPos.x, -500.0f, 500.0f);
        tmpPos.y = Mathf.Clamp(tmpPos.y, -500.0f, 500.0f);
        transform.localPosition = tmpPos;
    }

    public void Click()
    {
        Debug.Log("Click called!");

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 0.5f, Vector3.forward);


        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.GetComponent<Clickable>())
            {
                hit.collider.gameObject.GetComponent<Clickable>().Click();
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
