using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 5.0f;
    public float spawnTimer = 0.0f;
    public bool moveObstacle = false;
    void Update()
    {
        spawnTimer += Time.deltaTime;
        float random = spawnRate+Random.Range(-2.0f, 2.0f);
        if (spawnTimer >= random)
        {
            GameObject newObstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
            newObstacle.GetComponent<Obstacle>().isMoving = moveObstacle;
            newObstacle.transform.SetParent(transform.parent);
            spawnTimer = 0.0f;
        }
    }
}
