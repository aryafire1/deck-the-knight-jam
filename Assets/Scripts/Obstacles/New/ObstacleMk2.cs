using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMk2 : MonoBehaviour
{
    public int damage = 1;
    public SpriteRenderer sprite;
    public float delay = -1;

    bool delayedHitbox = false;
    float timer = 0;

    public void Initialize(int dmg)
    {
        if (delay != -1)
        {
            delayedHitbox = true;
        }

        damage = dmg;

        if (damage > 1 && sprite)
        {
            sprite.color = new Color(255, 0, 100);
        }
    }

    private void Update()
    {
        if (delayedHitbox)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                GetComponent<BoxCollider2D>().enabled = true;
            }
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
