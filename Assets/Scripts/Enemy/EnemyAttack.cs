using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake ()
    {   
        //mencari game object dengan tag player
        player = GameObject.FindGameObjectWithTag ("Player");

        //dapatkan component player health
        playerHealth = player.GetComponent<PlayerHealth>();

        //dapatkan komponen animator
        anim = GetComponent<Animator>();

        //mendapatkan Enemy health
        enemyHealth = GetComponent<EnemyHealth>();
    }  

    //callback jika ada suatu object masuk dalam trigger
    void OnTriggerEnter(Collider other)
    {   
        //set play not in range
        if(other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        //mentrigger animasi PlayerDead jika darah player kurang dari sama dengan 0
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }


    void Attack()
    {   
        //reset time
        timer = 0f;

        //taking dead
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
