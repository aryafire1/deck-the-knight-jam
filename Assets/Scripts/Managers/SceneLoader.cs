using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadScene("proto");
    }
    public void MenuScene(){
        SceneManager.LoadScene("MainScreen");
    }
}
