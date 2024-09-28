using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollectable : MonoBehaviour
{
    public Card card;

    CardManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = CardManager.Singleton; // Gets the card manager
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (manager != null && manager.Add(card))
        {
            Destroy(gameObject);
        }
    }
}
