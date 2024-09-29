using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 10.0f;
    public Vector3 target;
    public bool isMoving = false;
    public int orbType = 0;//0 no orb, 1 meteorite, 2 Chromatic orb
    private Player player;
    public int damage = 1;
    public float disappearTime = 0.0f;
    public bool spellSlot = false;
  

    void Start()
    {
        if (orbType == 1 || orbType == 2){
            player = FindObjectOfType<Player>();
            Vector3 temp = player.transform.position;
            target = temp;
            transform.rotation = pointToPlayer();
        }
        else{
            target = transform.parent.position;
            transform.parent = null;
        }
        if(spellSlot){
            GameManager.ChangeSpellSlot(-1);
        }
        
    }
    Quaternion pointToPlayer(){
        Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(new Vector3(0, 0, angle+90));
    }

    // Update is called once per frame
    void Update()
    {
        if (orbType == 2){
            target = player.transform.position;
            StartCoroutine(Disappear(6.0f));
        }
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                Destroy(gameObject);
            }
            if(disappearTime > 0){

                StartCoroutine(Disappear(disappearTime));
            }
        }
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            other.gameObject.GetComponent<Player>().takeDamage(damage);
            if (isMoving){
                Destroy(gameObject);
            }
        }
    }
    IEnumerator Disappear(float time)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(time-1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
