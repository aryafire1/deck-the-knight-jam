using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 5.0f;
    public float spawnTimer = 0.0f;
    public bool moveObstacle = false;
    public bool randomizeY = false;
    public bool randomizeX = false;
    public Vector3 originalPosition;
    public Vector3 originalParetnPosition;
    public void Start()
    {
        originalPosition = transform.position;
        originalParetnPosition = transform.parent.position;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        float random = spawnRate+Random.Range(-2.0f, 2.0f);
        if (spawnTimer >= random)
        {
            if (randomizeY){
                float r = Random.Range(-1f, 1f);
                transform.parent.position = new Vector3(originalParetnPosition.x, originalParetnPosition.y+r, originalParetnPosition.z);
            }
            else if (randomizeX){
                
                transform.position = new Vector3(originalParetnPosition.x+Random.Range(2f, 22f), transform.position.y, transform.position.z);
                
            }
            GameObject newObstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
            newObstacle.GetComponent<Obstacle>().isMoving = moveObstacle;
            newObstacle.transform.SetParent(transform.parent);
            spawnTimer = 0.0f;
        }
    }
}
