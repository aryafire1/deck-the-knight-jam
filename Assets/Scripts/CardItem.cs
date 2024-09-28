using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Card;
using UnityEngine.Events;

public class CardItem : MonoBehaviour
{

    public UnityAction<CardType> OnPositive;
    public UnityAction<CardType> OnNegative;
    public Card card;


    bool used = false; // Whether the card has been used or not

    public virtual float Use() // Call this method twice: one for positive effect, two for negative effect
    {
        if (!used)
        {
            used = true;
            Debug.Log("positive");
            OnPositive?.Invoke(card.posType);
        }
        return card.duration;
    }

    public virtual void NegativeEffect()
    {
        Debug.Log("negative");
        OnNegative?.Invoke(card.negType);
    }
}
