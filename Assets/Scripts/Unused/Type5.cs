using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type5 : MonoBehaviour, ICard
{
    int result;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Roll(int max) {
        result = Random.Range(1, max + 1);
    }

    public void Result() {
        if (result == 1) {
            Positive();
        }
        else if (result == 2) {
            Negative();
        }
    }

    public void Positive() {
            Debug.Log("Positive");
    }

    public void Negative() {
            Debug.Log("Negative");
    }
}
