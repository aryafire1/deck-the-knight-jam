using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 10.0f;
    public Vector3 target;
    public bool isMoving = false;
    public int orbType = 0;//0 no orb, 1 meteorite, 2 Chromatic orb
    private Player player;
  

    void Start()
    {
        if (orbType == 1 || orbType == 2){
            player = FindObjectOfType<Player>();
            Vector3 temp = player.transform.position;
            target = temp;
        }
        else{
            target = transform.parent.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (orbType == 2){
            target = player.transform.position;
            StartCoroutine(Disappear(6.0f));
        }
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
            StartCoroutine(Disappear(1.0f));
        }
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player hit obstacle");
            if (isMoving){
                Destroy(gameObject);
            }
        }
    }
    IEnumerator Disappear(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
