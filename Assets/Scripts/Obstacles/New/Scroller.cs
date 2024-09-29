using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Scroller : MonoBehaviour
{
    public float speed = 10;
    public float timer = 30f;
    public Transform destination;

    // Update is called once per frame
    void Update()
    {
        if (destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, destination.position) < 0.001f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Initialize(Transform dest)
    {
        destination = dest;
    }
}
