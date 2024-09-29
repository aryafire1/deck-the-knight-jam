using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMk2 : MonoBehaviour
{
    public int damage = 1;
    public SpriteRenderer sprite;

    private void Start()
    {
        if (damage > 1 && sprite)
        {
            sprite.color = new Color(255, 0, 100);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            other.gameObject.GetComponent<Player>().takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
