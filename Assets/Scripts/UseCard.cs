using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UseCard : MonoBehaviour
{
    public static UseCard Singleton;
    private static CardManager manager;
    public static int cardIndex = 0;
    public static Image image;
    // Start is called before the first frame update
    void Start()
    {
        manager = CardManager.Singleton;
        Singleton = this;
        image = GetComponent<Image>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (manager.cardList.Count > 0)
            {
                manager.cardList[0].Use();
                manager.cardList.RemoveAt(0);
                AddCardUI();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (manager.cardList.Count > 1)
            {
                manager.cardList[1].Use();
                manager.cardList.RemoveAt(1);
                AddCardUI();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (manager.cardList.Count > 2)
            {
                manager.cardList[2].Use();
                manager.cardList.RemoveAt(2);
                AddCardUI();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (manager.cardList.Count > 3)
            {
                manager.cardList[3].Use();
                manager.cardList.RemoveAt(3);
                AddCardUI();
            }
        }
    }
    public static void AddCardUI(){
        int c = manager.cardList.Count;

        for (int i = 0; i < c; i++)
        {
            Transform child = Singleton.transform.GetChild(i);
            child.gameObject.SetActive(true);
            image = child.GetComponent<Image>();
            image.sprite = manager.cardList[i].icon;
        }
        for (int i = 4; i > c; i--)
        {
            Transform child = Singleton.transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
    }
    
}
