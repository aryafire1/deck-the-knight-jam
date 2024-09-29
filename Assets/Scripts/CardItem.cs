using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Card;
using UnityEngine.Events;

public class CardItem : MonoBehaviour
{
    public static CardItem cardItem;

    public UnityAction<CardType> OnPositive;
    public UnityAction<CardType> OnNegative;
    public Card card;


    bool used = false; // Whether the card has been used or not

    void Start() {
        cardItem = this;
    }

    public virtual float Use() // Applies positive effect to the player
    {
        if (!used)
        {
            used = true;
            Debug.Log("positive");
            OnPositive?.Invoke(card.posType);
        }
        return card.duration;
    }

    public virtual void NegativeEffect() // Applies negative effect to the player
    {
        Debug.Log("negative");
        OnNegative?.Invoke(card.negType);
    }
}
