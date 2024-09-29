using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player player;
    public Rigidbody2D rb;
    public int health = 3;
    
    public Transform[] positions = new Transform[4];
    public float speed = 5.0f;
    public float jumpHeight = 6.5f;
    private KeyCode[] keys2 = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };
    public float dashCooldown = 1.0f;
    private float dashTimer = 0.0f;
    private bool isGrounded = false;
    private bool airJump = false;
    public float invulTime= 1.0f;
    private float invulTimer= 0.0f;
    public Slider hpSlider;
    public Animator anim;
    public GameObject particle;
    void Start()
    {
        anim = GetComponent<Animator>();
        hpSlider.maxValue = health;
        hpSlider.value = health;
        player = this;
        rb = GetComponent<Rigidbody2D>();
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
            if (isGrounded && rb.velocity.y < 5)
            {
                rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
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
        //dashBlink(move, jump);\
        dashMove(move, jump);
        transform.parent.position += move * speed * Time.deltaTime;
    }
    public void dashBlink(Vector3 move, bool jump){
        if (dashTimer >= dashCooldown && Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(parti(false));
            Vector3 tempPosition = positions[0].position;
            if (jump)
            {
                 if (move.x == -1){
                    tempPosition.x = positions[2].position.x;
                }
                else if (move.x == 1){
                    tempPosition.x = positions[3].position.x;
                }
                anim.SetBool("isGrounded", false);
            }
            else if (move.x != 0)
            {
                tempPosition = (move.x == -1) ? positions[2].position : positions[3].position;
            }
            transform.parent.position = tempPosition;
            dashTimer = 0.0f;
            StartCoroutine(parti(true));
        }
    }
    public void dashMove(Vector3 move, bool jump){
        if (dashTimer >= dashCooldown && Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(parti(false));
            Vector2 tempPosition = new Vector2(0, 0);
            if (jump && airJump)
            {
                tempPosition.x = move.x*5;
                tempPosition.y = jumpHeight*1.5f;
                anim.SetBool("isGrounded", false);
                airJump = false;
            }
            else if (move.x != 0)
            {
                tempPosition.x = move.x*5;
            }
            invulTimer = invulTime/2;
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0;
            rb.velocity = tempPosition;
            
            //rb.AddForce(tempPosition, ForceMode2D.Impulse);
            dashTimer = 0.0f;
            StartCoroutine(parti(true));
        }
    }
    public void takeDamage(int damage)
    {
        if (invulTimer <= 0.0f)
        {
            health -= damage;
            invulTimer = invulTime;
            hpSlider.value = health;
            if (health <= 0)
            {
                //Debug.Log("dead");
                GameManager.manager.GameOver();
            }
            StartCoroutine(Flash(5));
        }
    }
    public void becomeInvul(float time)
    {
        invulTimer = time;
        StartCoroutine(Flash(time));
    }
    IEnumerator Flash(float num)
    {
        for (int i = 0; i < num; i++)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator parti(bool follow)
    {
        GameObject temp = Instantiate(particle, transform.position, Quaternion.identity);
        if (follow)
        {
            temp.transform.parent = transform;
            temp.transform.position = transform.position;
            temp.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            StartCoroutine(Flash(3));
            yield return new WaitForSeconds(0.2f);
            rb.gravityScale = 2.5f;
            
        }
        else{
            yield return new WaitForSeconds(0.3f);
        }
        Destroy(temp);

    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && collision.gameObject.transform.position.y+0.3f < transform.position.y)
        {
            isGrounded = true;
            airJump = true;
            anim.SetBool("isGrounded", true);
        }
    }
}
