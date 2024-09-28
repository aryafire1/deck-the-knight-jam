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
    public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        anim.SetFloat("SpeedR", 1f);
        Vector3 move = new Vector3(0, 0, 0);
        bool jump = false;
        if (Input.GetKey(keys2[0]))
        {
            jump = true;
            if (isGrounded)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5.5f), ForceMode2D.Impulse);
                isGrounded = false;
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
            move.x = -1;
            anim.SetFloat("SpeedR", 0.7f);
            
        }
        if (Input.GetKey(keys2[3]))
        {
            move.x = 1;
            anim.SetFloat("SpeedR", 1.5f);
        }
        dashTimer += Time.deltaTime;
        dash(move, jump);
        transform.parent.position += move * speed * Time.deltaTime;
    }
    public void dash(Vector3 move, bool jump){
        if(dashTimer >= dashCooldown && Input.GetKey(KeyCode.Space)){
            if (jump){
            Vector3 temp = positions[0].position;
            if (move.x == -1){
                temp.x = positions[2].position.x;
            }
            else if (move.x == 1){
                temp.x = positions[3].position.x;
            }
            transform.parent.position = temp;
            }
            else if (move.x == -1){
                Vector3 temp = positions[2].position;
                temp.x = positions[2].position.x;
                transform.parent.position = temp;
            }
            else if (move.x == 1){
                Vector3 temp = positions[3].position;
                temp.x = positions[3].position.x;
                transform.parent.position = temp;

            }
            dashTimer = 0.0f;
        }
    }
    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
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
        }
    }
}
