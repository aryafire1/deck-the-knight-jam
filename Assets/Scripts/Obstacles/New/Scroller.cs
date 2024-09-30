using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Scroller : MonoBehaviour
{
    public float speed = 10;
    public float timer = 30f;
    public Transform destination;
    public List<Transform> cardSpawn = new List<Transform>();
    public GameObject cardObject;

    bool spawnCard = false;
    float aliveTime = 0;

    // Update is called once per frame
    void Update()
    {
        aliveTime += Time.deltaTime;
        if (destination != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, destination.position) < 0.001f || aliveTime >= timer)
            {
                if (destination.transform.CompareTag("Temporary"))
                {
                    Destroy(destination.gameObject);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (-transform.up * 100), speed * Time.deltaTime);
            if (aliveTime >= timer)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Initialize(Transform dest, bool cardSpawner)
    {
        destination = dest;
        spawnCard = cardSpawner;
        if (spawnCard)
        {
            Debug.Log("spawn");
            int random = Random.Range(0, cardSpawn.Count);
            GameObject card = Instantiate(cardObject, cardSpawn[random].position, Quaternion.identity, transform);
        }
    }
}
