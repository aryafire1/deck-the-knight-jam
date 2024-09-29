using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCard : MonoBehaviour
{
    public GameObject cardPrefab;
    void Start()
    {
        int r = Random.Range(0, 4);
        if (r == 0)
        {
            Transform placer = transform.GetChild(0);
            GameObject card = Instantiate(cardPrefab, placer.position, Quaternion.identity);
            card.transform.SetParent(placer);
        }
    }
}
