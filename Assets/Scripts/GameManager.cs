using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public int score = 40;
    private float scoreTimer = 0.0f;
    public int scoreBoss = 50;
    public int highScore = 0;
    public bool isPaused = false;
    public bool isGameOver = false;
    public bool bossFight = false;

    public Spawner spawner;
    public GameObject prefabFireball;
    public GameObject box;
    public int maxSpellSlots = 20;
    public static int spellSlots = 20;

    void Start()
    {
        manager = this;
    }

    // Update is called once per frame
    void Update()
    {
        scoreTimer += Time.deltaTime;
        if(spellSlots <= 0){
            StopBossFight();
        }
        else if (scoreTimer >= 1.0f)
        {
            score += 1;
            scoreTimer = 0.0f;
        }
        if(scoreBoss <= score){
            StartBossFight();
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            PauseGame();
        }
        

    }
    public void StartBossFight(){
        bossFight = true;
        
        transform.GetChild(0).gameObject.SetActive(true);
        spawner.obstaclePrefab = prefabFireball;
    }
    public void StopBossFight(){
        bossFight = false;
        spellSlots = maxSpellSlots;
        transform.GetChild(0).gameObject.SetActive(false);
        spawner.obstaclePrefab = box;
        scoreBoss = score + 50;
    }
    public void GameOver(){
        isGameOver = true;
        if(score > highScore){
            highScore = score;
        }
        PauseGame();
    }
    public void PauseGame(){
        isPaused = !isPaused;
        if(isPaused){
            Time.timeScale = 0;
        }
        else{
            Time.timeScale = 1;
        }
    }

}
