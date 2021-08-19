using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notificationbox : MonoBehaviour
{

    public Vector2 SpawnBounds;
    public string Message;

    public TMP_Text Text;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localPosition = new Vector3(Random.Range(-SpawnBounds.x, SpawnBounds.x), Random.Range(-SpawnBounds.y, SpawnBounds.y), 0);

    }

    // Update is called once per frame
    void Update()
    {
        Text.text = Message;
    }
}
