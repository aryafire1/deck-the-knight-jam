using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Wizard : MonoBehaviour
{
    private Player player;
    public float prevX;
    private Animator animator;
    public GameObject spawner;
    public AudioSource attackSound;
    public float attackRate = 5f;
    public float attackMod = 2f;

    public void Start()
    {
        player = FindObjectOfType<Player>();
        prevX = player.transform.position.x;
        animator = GetComponent<Animator>();
    }
    public void Update()
    {
        float atkFlux = Random.Range(-attackMod, attackMod);


        transform.position = Vector3.MoveTowards(transform.position, spawner.transform.position, 2 * Time.deltaTime);
        Vector3 temp = player.transform.position;
        
        if (temp.x > transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void onDisable(){
        transform.position = transform.position + new Vector3(0, 3, 0);
    }

    public void attack(){
        attackSound.Play();
        
        StartCoroutine(AttackAnimation(1));
    }

    IEnumerator AttackAnimation(float attackDelay)
    {

        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.6f);
        animator.SetBool("isAttacking", false);
    }
}
