using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMk2 : MonoBehaviour
{
    public List<GameObject> obstacles = new List<GameObject>();
    public Transform destroyer;

    GameManager gameManager;
    public bool canSpawnCard = true;
    public float scoreDividend = 0;

    private void Start()
    {
        gameManager = GameManager.manager;
        Spawn();
    }

    void Spawn()
    {
        float score = gameManager.score / 10;
        Debug.Log("Score: " + score + " | Dividend: " + scoreDividend);
        if (score - 1 >= scoreDividend)
        {
            Debug.Log("yay");
            canSpawnCard = true;
            scoreDividend = score;
        }
        else
        {
            canSpawnCard = false;
        }

        int random = Random.Range(0, obstacles.Count);
        GameObject newObstacle = Instantiate(obstacles[random], transform.position, Quaternion.identity);
        Scroller obstacle = newObstacle.GetComponent<Scroller>();
        obstacle.Initialize(destroyer, canSpawnCard);
        StartCoroutine(WaitToSpawn(newObstacle.GetComponent<BoxCollider2D>().size.x, newObstacle.GetComponent<Scroller>().speed));
    }

    IEnumerator WaitToSpawn(float size, float speed)
    {
        yield return new WaitForSeconds(size/speed);
        Spawn();
    }
}
