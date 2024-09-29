using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public int score = 40;
    private float scoreTimer = 0.0f;
    public int scoreBoss = 50;
    public static int highScore = 0;
    public bool isPaused = false;
    public bool isGameOver = false;
    public bool bossFight = false;

    public Spawner spawner;
    public GameObject prefabFireball;
    public GameObject box;
    public GameObject pauseMenu;
    public GameObject endGameMenu;
    public Text highScoreText;
    public Text scoreText;
    public Slider musicSlider;
    public AudioSource Audios;
    public int maxSpellSlots = 10;
    [SerializeField]
    public static int spellSlots = 10;

    private void Awake()
    {
        manager = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        endGameMenu.SetActive(false);
        pauseMenu.SetActive(false);
        Audios = transform.GetComponent<AudioSource>();
        Audios.volume = musicSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        scoreTimer += Time.deltaTime;
        if (scoreTimer >= 1.0f)
        {
            score += 1;
            scoreText.text = "" + score;
            scoreTimer = 0.0f;
        }
        if(scoreBoss <= score && bossFight == false){
            StartBossFight();
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            PauseGame();
        }
        

    }
    
    public static void UseSpellSlot(){
        spellSlots -= 1;
        Debug.Log("Spell Slots: " + spellSlots);
        if(spellSlots <= 0){
            manager.StopBossFight();
        }
    }
    public void StartBossFight(){
        bossFight = true;
        spellSlots = maxSpellSlots;
        transform.GetChild(0).gameObject.SetActive(true);
        spawner.obstaclePrefab = prefabFireball;
        spawner.spawnRate = 3.5f;
    }
    public void StopBossFight(){
        bossFight = false;
        maxSpellSlots +=5;
        spellSlots = maxSpellSlots;
        transform.GetChild(0).gameObject.SetActive(false);
        spawner.obstaclePrefab = box;
        spawner.spawnRate = 5.0f;
        scoreBoss = score + 50;
    }
    public void GameOver(){
        endGameMenu.SetActive(true);
        Time.timeScale = 0;
        isGameOver = true;
        if(score > highScore){
            highScore = score;
        }
        highScoreText.text = "High Score: " + highScore;
        PauseGame();
    }
    public void PauseGame(){
        if(isGameOver == false){
            isPaused = !isPaused;
            if(isPaused){
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            else{
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                Audios.volume = musicSlider.value;
            }
        }
    }

}
