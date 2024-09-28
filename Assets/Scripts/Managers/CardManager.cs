using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // Allows CardManager to be accessed by name from other scripts
    #region Singleton

    private static CardManager _singleton;
    public static CardManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
                DontDestroyOnLoad(value);
            }
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(CardManager)} instance already exists, destoying duplicate");
                Destroy(value);
            }
        }
    }

    private void Awake()
    {
        Singleton = this;
    }

    #endregion

    public delegate void OnCardChanged(); // When called, calls every method subscribed. Used to update inventory UI
    public OnCardChanged onCardChangedCallback;

    public int space = 0;

    Dictionary<Card, int> cardList = new Dictionary<Card, int>();

    /*
    Type1 type1 = new Type1();
    Type2 type2 = new Type2();
    Type3 type3 = new Type3();
    Type4 type4 = new Type4();
    Type5 type5 = new Type5();
    */

    void Start()
    {
        

    }

    public bool Add(Card newCard) // Adds new card that player collects
    {
        if (cardList.ContainsKey(newCard) && cardList[newCard] >= space) // If the player has too many of the same cards, don't add new card
        {
            Debug.Log("Not enough room for this card");
            return false;
        }

        if (cardList.ContainsKey(newCard)) // If the player already has some of this card
        {
            cardList[newCard]++;
        }
        else // Collect card for the first time
        {
            cardList.Add(newCard, 1);
        }

        CallbackMethod(); // Update UI
        return true;
    }

    public void Remove(Card newCard) // Removes card upon usage
    {
        if (cardList[newCard] == 1) // Clears card from inventory if all cards used up
        {
            cardList.Remove(newCard);
        }
        else // Removes 1 from stockpile of cards
        {
            cardList[newCard]--;
        }

        CallbackMethod(); // Update UI
    }

    void CallbackMethod()
    {
        if (onCardChangedCallback != null)
        {
            onCardChangedCallback.Invoke();
        }
    }

    void Update() {
        this.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime,0,0);

        if (Input.GetKeyDown(KeyCode.F)) {
            string result = "List contents: ";
            foreach (Card card in cardList.Keys)
            {
                result += card.name + ", ";
            }
            Debug.Log(result);
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            foreach(Card card in cardList.Keys)
            {
                card.Use();
                Debug.Log($"Card {card} used");
            }

        }
    }
}
