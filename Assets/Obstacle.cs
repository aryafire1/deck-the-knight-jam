using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 10.0f;
    public Vector3 target;
    public bool isMoving = false;

    void Start()
    {
        target = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                Destroy(gameObject);
            }
        }
        else{
            Vector3 temp = transform.position;
            temp.y = temp.y/2;
            transform.position = temp;
            //dispear after 1 second
            StartCoroutine(Disappear());
        }
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player hit obstacle");
        }
    }
    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
