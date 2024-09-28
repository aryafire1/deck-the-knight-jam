using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public int cardTypeAmount;
    List<int> cardList = new List<int>();

    Type1 type1 = new Type1();
    Type2 type2 = new Type2();
    Type3 type3 = new Type3();
    Type4 type4 = new Type4();
    Type5 type5 = new Type5();

    void Start() {
        cardTypeAmount = cardTypeAmount + 1;
    }

    void OnTriggerEnter2D(Collider2D other) {
            cardList.Add(Random.Range(1, cardTypeAmount));
            Debug.Log("trigger");
    }

    void Update() {
        this.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime,0,0);

        if (Input.GetKeyDown(KeyCode.F)) {
            string result = "List contents: ";
            foreach (var item in cardList)
            {
                result += item.ToString() + ", ";
            }
            Debug.Log(result);
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            type1.Roll(2);
            type1.Result();
        }
    }
}
