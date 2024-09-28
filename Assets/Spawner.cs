using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 5.0f;
    public float spawnTimer = 0.0f;
    public bool moveObstacle = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            GameObject newObstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
            newObstacle.GetComponent<Obstacle>().isMoving = moveObstacle;
            newObstacle.transform.SetParent(transform.parent);
            spawnTimer = 0.0f;
        }
    }
}
