using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public float health = 50f;

    public Transform enemy;

    public float shootingRange = 100f;

    //Declaration of variables to be used in pathfinding
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;

    //Declaration of destination co-ordinates, a boolean checking if a destination exists, and the range which each agent aims to move
    public Vector3 destination;
    bool destinationAlreadySet;
    public float destinationRange;

    //This sets how far away the agents can see and attack Tyler
    public float sightRange, attackRange;
    public bool tylerInSight, tylerInAttack;

    private Animator animator;

    public GameObject[] finalEnemies;

    public Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        finalEnemies = GameObject.FindGameObjectsWithTag("FinalEnemy");
        Debug.Log("FINAL ENEMIES" + finalEnemies.Length);
        
for (int x = 0; x < finalEnemies.Length; x++)
        {
            finalEnemies[x].SetActive(false);
        }

        //rigidBody = GetComponent<Rigidbody>();
        //rigidBody.freezeRotation = true;
    }

    private void Update()
    {
        

        //rigidBody.freezeRotation = true;
        //Sets a variable as true depending on if the player is in sight or close enough to attack
        tylerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        tylerInAttack = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!tylerInSight && !tylerInAttack)
        {
            Patrol();
        }
        if (tylerInSight && !tylerInAttack)
        {
            MoveCloser();
        }
        if (tylerInSight && tylerInAttack)
        {
            Shoot();
        }


        //Recalculates the AI's path if it ends up blocked, allowing dynamic changes
        if (rigidBody.velocity == Vector3.zero)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (!destinationAlreadySet)
        {
            CreateDestination();
        }
        else
        {
            agent.SetDestination(destination);
            Vector3 distanceToDestination = transform.position - destination;
            animator.SetFloat("Speed", 20);
            animator.SetBool("Aim", false);

            if (distanceToDestination.magnitude < 1f)
            {
                destinationAlreadySet = false;

            }
        }
    }

    private void CreateDestination()
    {
        float randomDestinationZ = Random.Range(-destinationRange, destinationRange);
        float randomDestinationX = Random.Range(-destinationRange, destinationRange);
        destination = new Vector3(transform.position.x + randomDestinationX, transform.position.y, transform.position.z + randomDestinationZ);


        if (Physics.Raycast(destination, -transform.up, 2f, groundLayer))
        {
            destinationAlreadySet = true;
        }

    }


    private void MoveCloser()
    {
        agent.SetDestination(player.position);
        animator.SetFloat("Speed", 20);
        animator.SetBool("Aim", false);
    }
    private void Shoot()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        animator.SetFloat("Speed", 0);
        animator.SetBool("Aim", true);

        RaycastHit hit;

        if (Physics.Raycast(enemy.transform.position, enemy.transform.forward, out hit, shootingRange))
        {
            if (Accuracy() == 1)
            {
                
                Shoot playerShoot = hit.transform.GetComponent<Shoot>();
                playerShoot.TakeDamage(1);
            }
        }
    }

        public void TakeDamage(float amount)
    {

        health -= amount;
        if (health <= 0)
        {
            AIDie();
        }

    }

    void AIDie()
    {
        Destroy(gameObject);
    }

    int Accuracy()
    {
        if (PlayerPrefs.GetInt("Easy") == 1)
        {
            int randomRange = Random.Range(0, 200);
            Debug.Log(randomRange);
            return randomRange;
        }
        else
        {
            int randomRange = Random.Range(0, 100);
            Debug.Log(randomRange);
            return randomRange;
        }
        }



    //Recalculate AI route if collides, leading to dynamic changing
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("Enemy collision");
        CreateDestination();
        Patrol();

    }
   
}
