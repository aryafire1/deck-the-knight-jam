using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UIElements.UxmlAttributeDescription;

[CreateAssetMenu(fileName = "New Card", menuName = "Game Jam/Card")]
public class Card : ScriptableObject
{
    public enum CardType
    {
        Attack = 0,
        Shield = 1,
        Health = 2,
        Stun = 3,
        Gamble = 4,
        Anger = 5,
        Slow = 6,
        WizHeal = 7,
        WizStun = 8,
        WizGamble = 9,
    }
    public UnityAction<CardType> OnPositive;
    public UnityAction<CardType> OnNegative;

    public CardType posType = CardType.Attack;
    public CardType negType = CardType.Slow;
    public string cardName = "";
    public Sprite icon;

    [Tooltip("The duration of the effect (Ex: how long the shield lasts)")]
    public float duration = 0;
    
    [Tooltip("The magnitude of effect (Ex: how much health recovered)")]
    public float effectAmount = 0;

    public virtual float Use() // Applies positive effect to the player
    {
        Debug.Log("positive");
        OnPositive?.Invoke(posType);
        return duration;
    }

    public virtual void NegativeEffect() // Applies negative effect to the player
    {
        Debug.Log("negative");
        OnNegative?.Invoke(negType);
    }
}
