using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public Transform end;
    public Transform start;
    public float speed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, end.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, end.position) < 0.001f)
        {
            transform.position = start.position;
        }
    }
}
