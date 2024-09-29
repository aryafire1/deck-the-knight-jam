using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;

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

    Dictionary<CardItem, int> cardList = new Dictionary<CardItem, int>();

    bool timer = true; // Makes sure we can't spam card activations

    public bool Add(CardItem newCard) // Adds new card that player collects
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

    public void Remove(CardItem newCard) // Removes card upon usage
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
            foreach (CardItem card in cardList.Keys)
            {
                result += card.name + ", ";
            }
            Debug.Log(result);
        }

        if (timer && Input.GetKeyDown(KeyCode.T)) {
            timer = false;
            foreach(CardItem card in cardList.Keys)
            {
                UnityAction cardAction = (UnityAction)Delegate.CreateDelegate(typeof(UnityAction), this, card.card.posType.ToString()); // Gets card effect name, matches it with card effect method
                card.OnPositive += delegate { cardAction(); }; // Actually makes the code run for positive effect
                UnityAction negCardAction = (UnityAction)Delegate.CreateDelegate(typeof(UnityAction), this, card.card.negType.ToString()); // Gets card effect name, matches it with card effect method
                card.OnNegative += delegate { negCardAction(); }; // Actually makes the code run for negative effect
                StartCoroutine(WaitForEffectToEnd(card, card.Use())); // Waits for effect to end
            }
        }
    }

    private IEnumerator WaitForEffectToEnd(CardItem playedCard, float duration) // Do the negative effect after the positive effect
    {
        yield return new WaitForSeconds(duration);
        playedCard.NegativeEffect(); // Does negative effect for the card
        timer = true;
        Remove(playedCard); // Removes card from inventory
    }

    #region Card Effects
    // Make sure the method has the same name as the cardType name in the Card scriptable object
/*
    public void Shield()
    {
        Player.invulTimer = Card.duration;

        Debug.Log("shield");
    }

    public void Speed()
    {
        Player.speed = Player.speed * 2;

        Debug.Log("speed");
    }

    public void Slow()
    {
        Player.speed = Player.speed * 0.5f;

        Debug.Log("slow");
    }
*/
    #endregion
}
