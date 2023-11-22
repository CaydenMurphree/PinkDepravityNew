using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform playerTran;
    public Player player; // Reference to the Player script
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    public int damageAmount = 10; // Amount of damage the enemy inflicts

    public int reverseDistance;


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange, reversing;

    public int doDamage;

    Vector3 backupPoint;


    // Start is called before the first frame update
    void Awake()
    {
        playerTran = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.radius = 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (reversing)
        {
            backOff();
        }
        else
        {
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(playerTran.position);

    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(playerTran);

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        //if (health <= 0) Invoke(nameof(DestroyEnemy), .5f);
    }

    private void backOff()
    {
        agent.SetDestination(backupPoint);

        //if walkpoint reached 
        Vector3 distanceToWalkPoint = transform.position - backupPoint;
        if (distanceToWalkPoint.magnitude < 3f)
        {
            reversing = false;
        }



    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision");
        //Debug.Log("Tag of collided object: " + other.gameObject.tag);

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit");
            if (!alreadyAttacked)
            {


                // Get the Player component
                //Player player = other.gameObject.GetComponent<Player>();
                //PlayerCollision playerCollision = other.gameObject.GetComponent<PlayerCollision>();

                if (player != null)
                {
                    // Ensure the TakeDamage method exists in the Player script

                    player.TakeDamage(damageAmount);

                    // make the enemy back off
                    Vector3 backupPoint = transform.position - transform.forward * reverseDistance;
                    reversing = true;

                    //playerCollision.SetEnemyCollision(true);
                }
                else
                {
                    //Debug.LogError("Player script not found on the collided object.");

                }

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }



}
