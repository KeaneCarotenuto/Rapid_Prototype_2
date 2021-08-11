using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperTask : MonoBehaviour
{
    bool crumpled = false;

    public GameObject stamp;

    [SerializeField] Mesh sphereMesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Crumple()
    {
        if (crumpled) return;

        crumpled = true;
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        GetComponent<MeshFilter>().mesh = sphereMesh;
        GetComponent<MeshCollider>().sharedMesh = sphereMesh;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!crumpled && collision.transform == stamp.transform)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
