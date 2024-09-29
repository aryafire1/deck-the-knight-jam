using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardCollectable : MonoBehaviour
{
    public List<Card> cards;
    public Image icon;
    public Card card;

    CardManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = CardManager.Singleton; // Gets the card manager

        int randomCard = Random.Range(0, cards.Count);
        card = cards[randomCard];
        icon.sprite = card.icon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (manager != null && manager.Add(card)) // Makes sure the player has an inventory object and they don't have too many cards
        {
            Debug.Log("Picked up " + card.cardName);
            Destroy(gameObject); // Destroys object
        }
    }
}
