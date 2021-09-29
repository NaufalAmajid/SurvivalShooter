using UnityEngine;
 
public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;

    void Awake()
    {
        //cari game object dengan tag player
        player = GameObject.FindGameObjectWithTag("Player").transform;
 
        //Mendapatkan Reference component
        playerHealth = player.GetComponent<PlayerHealth>();

        enemyHealth = GetComponent<EnemyHealth>();

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
 
    void Update()
    {
        //memindahkan posisi player
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
           nav.SetDestination(player.position);
        }
        else //hentikan moving
        {
            nav.enabled = false;
        }
    }
}