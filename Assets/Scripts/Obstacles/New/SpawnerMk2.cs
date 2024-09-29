using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMk2 : MonoBehaviour
{
    public List<GameObject> obstacles = new List<GameObject>();
    public Transform destroyer;

    private void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        int random = Random.Range(0, obstacles.Count);
        GameObject newObstacle = Instantiate(obstacles[random], transform.position, Quaternion.identity);
        Scroller obstacle = newObstacle.GetComponent<Scroller>();
        obstacle.Initialize(destroyer);
        StartCoroutine(WaitToSpawn(newObstacle.GetComponent<BoxCollider2D>().size.x, newObstacle.GetComponent<Scroller>().speed));
    }

    IEnumerator WaitToSpawn(float size, float speed)
    {
        yield return new WaitForSeconds(size/speed);
        Spawn();
    }
}
