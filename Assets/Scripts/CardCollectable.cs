using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollectable : MonoBehaviour
{
    CardItem cardItem;

    CardManager manager;

    // Start is called before the first frame update
    void Start()
    {
        cardItem = GetComponent<CardItem>();
        manager = CardManager.Singleton; // Gets the card manager
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
