using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Card", menuName = "Game Jam/Card")]
public class Card : ScriptableObject
{
    public enum CardType
    {
        Speed = 0,
        Shield = 1,
        Health = 2,
        Pos1 = 3,
        Pos2 = 4,
        Neg1 = 5,
        Slow = 6,
        Neg2 = 7,
        Neg3 = 8,
        Neg4 = 9,
    }

    public UnityAction<CardType> OnPositive;
    public UnityAction<CardType> OnNegative;

    public CardType posType = CardType.Speed;
    public CardType negType = CardType.Slow;
    public string cardName = "";
    [Tooltip("The duration of the effect (Ex: how long the shield lasts)")]
    public float duration = 0;
    [Tooltip("The magnitude of effect (Ex: how much health recovered)")]
    public float effectAmount = 0;

    private bool used = false; // Whether the card has been used or not

    public virtual float Use() // Call this method twice: one for positive effect, two for negative effect
    {
        if (!used)
        {
            used = true;
            Debug.Log("positive");
            OnPositive?.Invoke(posType);
        }
        return duration;
    }

    public virtual void NegativeEffect()
    {
            Debug.Log("negative");
            OnNegative?.Invoke(negType);
    }
}
