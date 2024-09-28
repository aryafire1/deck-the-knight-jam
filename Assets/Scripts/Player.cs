using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 3;
    public int score = 0;
    public KeyCode[] keys = { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
    public Transform[] positions = new Transform[4];
    float speed = 5.0f;
    public KeyCode[] keys2 = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };
    public float dashCooldown = 1.0f;
    public float dashTimer = 0.0f;
    private bool isGrounded = false;
    public float invulTime= 1.0f;
    public float invulTimer= 0.0f;
    public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        anim.SetFloat("SpeedR", 1f);
        
        Vector3 move = new Vector3(0, 0, 0);
        invulTimer -= Time.deltaTime;
        bool jump = false;
        if (Input.GetKey(keys2[0]))
        {
            jump = true;
            if (isGrounded)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5.5f), ForceMode2D.Impulse);
                isGrounded = false;
                anim.SetBool("isGrounded", false);
            }
        }
        if (Input.GetKey(keys2[1]))
        {
            if (!isGrounded)
            {
                move.y = -3;
            }

        }
        if (Input.GetKey(keys2[2]))
        {
            move.x = -1f;
            anim.SetFloat("SpeedR", 0.7f);
            
        }
        if (Input.GetKey(keys2[3]))
        {
            move.x = 1f;
            anim.SetFloat("SpeedR", 1.5f);
        }
        dashTimer += Time.deltaTime;
        dash(move, jump);
        transform.parent.position += move * speed * Time.deltaTime;
    }
    public void dash(Vector3 move, bool jump){
        if (dashTimer >= dashCooldown && Input.GetKey(KeyCode.Space))
        {
            Vector3 tempPosition = positions[0].position;
            if (jump)
            {
                tempPosition.x = (move.x == -1) ? positions[2].position.x : positions[3].position.x;
                anim.SetBool("isGrounded", false);
            }
            else if (move.x != 0)
            {
                tempPosition = (move.x == -1) ? positions[2].position : positions[3].position;
            }
            transform.parent.position = tempPosition;
            dashTimer = 0.0f;
        }
    }
    public void takeDamage(int damage)
    {
        if (invulTimer <= 0.0f)
        {
            health -= damage;
            invulTimer = invulTime;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            StartCoroutine(Flash());
        }
    }
    IEnumerator Flash()
    {
        for (int i = 0; i < 5; i++)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void addScore(int points)
    {
        score += points;
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && collision.gameObject.transform.position.y+0.8f < transform.position.y)
        {
            isGrounded = true;
            anim.SetBool("isGrounded", true);
        }
    }
}
