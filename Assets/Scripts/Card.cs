using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public CardType posType = CardType.Speed;
    public CardType negType = CardType.Slow;
    public string cardName = "";
    public float duration = 0; // The duration of the effect (Ex: how long the shield lasts)
    public float effectAmount = 0; // The magnitude of effect (Ex: how much health recovered)

    public virtual void Use()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            Debug.Log("Positive");
        }
        else
        {
            Debug.Log("Negative");
        }
    }
}
