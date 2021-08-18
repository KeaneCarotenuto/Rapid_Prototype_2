using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnDesk : MonoBehaviour
{
    [Header("Bounds")]
    public Vector2 yBounds = new Vector3( 0.5f, Mathf.Infinity );
    public Vector2 xBounds = new Vector3(-2.0f, 2.0f);
    public Vector2 zBounds = new Vector3(-2.0f, 2.0f);

    [Header("Respawning")]
    [SerializeField] bool autoSetPoint = true;
    public Vector3 respawnPoint;

    public float respawnHeight = 10.0f;
    public bool randomRotation = true;


    private Rigidbody m_rb;

    private void OnDrawGizmos()
    {
        Vector3 blfCorner = new Vector3(xBounds.x, yBounds.x, zBounds.x);
        Vector3 trbCorner = new Vector3(xBounds.y, yBounds.y, zBounds.y);

        Gizmos.DrawWireCube(
            (blfCorner + trbCorner) / 2.0f,
            new Vector3(Mathf.Abs(xBounds.x - xBounds.y), Mathf.Abs(yBounds.x - yBounds.y), Mathf.Abs(zBounds.x - zBounds.y))
            );
    }

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        if (autoSetPoint) respawnPoint = transform.position;
    }

    
    void Update()
    {
        if (
            (transform.position.y < yBounds.x) ||
            (transform.position.y > yBounds.y) ||
            (transform.position.x < xBounds.x) ||
            (transform.position.x > xBounds.y) ||
            (transform.position.z < zBounds.x) ||
            (transform.position.z > zBounds.y)
        )
        {
            if (m_rb) {
                m_rb.velocity = Vector3.zero;
                m_rb.angularVelocity = Vector3.zero;
            }

            transform.position = respawnPoint + new Vector3(0, respawnHeight, 0);
            if (randomRotation) transform.rotation = Random.rotation;
            else transform.rotation = Quaternion.identity;
        }
    }
}
