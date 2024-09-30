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
    public float bonusRate = 1;

    [Header("Attacks")]
    public int damage = 1;
    public int bonusDmg = 0;
    public GameObject prefabFireball;
    public GameObject prefabLightning;
    public float lightningRand = 5;
    public GameObject prefabMeteor;

    [Header("Attack Spawners")]
    public Transform lightningSpawner;
    public Transform meteorSpawner;
    public Transform fireballSpawner;
    
    SpawnerMk2 spawnerMk2;

    bool canAttack = false;
    public bool stunned = false;

    public void Start()
    {
        player = FindObjectOfType<Player>();
        prevX = player.transform.position.x;
        animator = GetComponent<Animator>();
        spawnerMk2 = SpawnerMk2.Singleton;
    }

    public void StartFight()
    {
        canAttack = true;
    }

    public void Update()
    {
        if (canAttack && !stunned)
        {
            canAttack = false;
            int atkFlux = Random.Range(-(int)attackMod -1, (int)attackMod +1);
            StartCoroutine(AttackAnimation((attackRate + atkFlux) / bonusRate));
        }


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

    void FireballAttack()
    {
        spawnerMk2.ManualSpawn(prefabFireball, damage + bonusDmg, fireballSpawner, spawnerMk2.destroyer);
        Debug.Log("fireball");
    }

    void LightningAttack()
    {
        float rand = Random.Range(-lightningRand, lightningRand);
        lightningSpawner.transform.position = new Vector2(player.transform.position.x + rand, lightningSpawner.transform.position.y);
        spawnerMk2.ManualSpawn(prefabLightning, damage + bonusDmg, lightningSpawner, spawnerMk2.destroyer);
        Debug.Log("lightning");
    }

    void MeteorAttack()
    {
        Vector3 vectorToTarget = player.transform.position - meteorSpawner.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        meteorSpawner.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        GameObject meteorTarget = new GameObject("Temporary");
        meteorTarget.transform.tag = "Temporary";
        GameObject target = Instantiate(meteorTarget, player.transform.position, Quaternion.identity);

        spawnerMk2.ManualSpawn(prefabMeteor, damage + bonusDmg, meteorSpawner, target.transform);
        Debug.Log("meteor");
    }

    IEnumerator AttackAnimation(float attackDelay)
    {
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            FireballAttack();
        }
        else if (rand == 1)
        {
            LightningAttack();
        }
        else
        {
            MeteorAttack();
        }
        GameManager.ChangeSpellSlot(-1);
        attackSound.Play();

        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.6f);
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(attackDelay - 0.6f);
        canAttack = true;
    }
}
