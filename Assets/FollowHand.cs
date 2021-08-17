using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHand : MonoBehaviour
{
    [SerializeField] GameObject hand;

    [SerializeField] Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vpp = cam.WorldToViewportPoint(hand.transform.position);

        if (Mathf.Abs(0.5f - vpp.x) > 0.25f) transform.position = Vector3.Lerp(transform.position, transform.position - transform.right * (0.5f - vpp.x) * Mathf.Abs(0.5f - vpp.x), 0.1f);

        //if (vpp.x < 0.25f) transform.position = Vector3.Lerp(transform.position, transform.position - transform.right, 0.01f);
        //if (vpp.x > 0.75f) transform.position = Vector3.Lerp(transform.position, transform.position + transform.right, 0.01f);
    }
}
