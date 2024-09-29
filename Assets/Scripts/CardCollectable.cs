using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardCollectable : MonoBehaviour
{
    public List<Card> cards;
    public Image icon;

    CardItem cardItem;

    CardManager manager;

    // Start is called before the first frame update
    void Start()
    {
        cardItem = GetComponent<CardItem>();
        manager = CardManager.Singleton; // Gets the card manager

        int randomCard = Random.Range(0, cards.Count);
        cardItem.card = cards[randomCard];
        icon.sprite = cardItem.card.icon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (manager != null && manager.Add(cardItem)) // Makes sure the player has an inventory object and they don't have too many cards
        {
            Debug.Log("Picked up " + cardItem.card.cardName);
            Destroy(gameObject); // Destroys object
        }
    }
}
