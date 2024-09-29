using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMk2 : MonoBehaviour
{
    #region Singleton

    public static SpawnerMk2 Singleton;

    private void Awake()
    {
        if (Singleton != null)
        {
            Debug.LogWarning("More than one instance of spawnerMk2 found!");
            return;
        }
        Singleton = this;
    }

    #endregion

    public List<GameObject> obstacles = new List<GameObject>();
    public Transform destroyer;

    GameManager gameManager;
    public bool canSpawnCard = true;
    public float scoreDividend = 0;
    public Wizard wizard;
    public GameObject wizardRoom;

    private void Start()
    {
        gameManager = GameManager.manager;
        Spawn();
    }

    void Spawn()
    {
        float score = gameManager.score / 10;
        if (score - 1 >= scoreDividend)
        {
            canSpawnCard = true;
            scoreDividend = score;
        }
        else
        {
            canSpawnCard = false;
        }

        if (wizard.isActiveAndEnabled)
        {
            GameObject newObstacle = Instantiate(wizardRoom, transform.position, Quaternion.identity);
            Scroller obstacle = newObstacle.GetComponent<Scroller>();
            obstacle.Initialize(destroyer, false);
            StartCoroutine(WaitToSpawn(newObstacle.GetComponent<BoxCollider2D>().size.x, newObstacle.GetComponent<Scroller>().speed));
        }
        else
        {
            int random = Random.Range(0, obstacles.Count);
            GameObject newObstacle = Instantiate(obstacles[random], transform.position, Quaternion.identity);
            Scroller obstacle = newObstacle.GetComponent<Scroller>();
            obstacle.Initialize(destroyer, canSpawnCard);
            StartCoroutine(WaitToSpawn(newObstacle.GetComponent<BoxCollider2D>().size.x, newObstacle.GetComponent<Scroller>().speed));
        }
    }

    public void ManualSpawn(GameObject obstacle)
    {
        GameObject newObstacle = Instantiate(obstacle, transform.position, Quaternion.identity);
        Scroller scroller = newObstacle.GetComponent<Scroller>();
        scroller.Initialize(destroyer, false);
    }

    IEnumerator WaitToSpawn(float size, float speed)
    {
        yield return new WaitForSeconds(size/speed);
        Spawn();
    }
}
