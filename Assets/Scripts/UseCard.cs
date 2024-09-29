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

        manager.onCardChangedCallback += AddCardUI;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (manager.cardList.Count > 0)
            {
                manager.UseCard(manager.cardList[0]);
                manager.Remove(manager.cardList[0]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (manager.cardList.Count > 1)
            {
                manager.UseCard(manager.cardList[1]);
                manager.Remove(manager.cardList[1]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (manager.cardList.Count > 2)
            {
                manager.UseCard(manager.cardList[2]);
                manager.Remove(manager.cardList[2]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (manager.cardList.Count > 3)
            {
                manager.UseCard(manager.cardList[3]);
                manager.Remove(manager.cardList[3]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (manager.cardList.Count > 4)
            {
                manager.UseCard(manager.cardList[4]);
                manager.Remove(manager.cardList[4]);
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
