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

    public GameObject soldierSkin;
    public GameObject horseSkin;

    [SerializeField] AudioSource hitSound;
    [SerializeField] AudioSource deathSound;


    // Start is called before the first frame update
    void Awake()
    {
        playerTran = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.radius = 0.1f;
        skinPrep();

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
        selectSkin();

    }

    private void skinPrep()
    {
        //soldierSkin = GameObject.Find("Soldier_demo");
        //horseSkin = GameObject.Find("Horse3D_Opt_Ver4");

        if (soldierSkin == null)
        {
            Debug.LogError("Soldier_demo not found");
            return;
        }

        if (horseSkin == null)
        {
            Debug.LogError("Horse3D_Opt_Ver4 not found");
            return;
        }

        if (player == null)
        {
            Debug.LogError("Player not found");
            return;
        }


    }

    private void selectSkin()
    {

        if (player.currentSanity < 50)
        {
            soldierSkin.SetActive(true);
            horseSkin.SetActive(false);
        }
        else
        {
            soldierSkin.SetActive(false);
            horseSkin.SetActive(true);
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
        if (health <= 0)
        {
            player.currentSanity += 60;
            deathSound.Play();
            Destroy(gameObject);
        }
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

    void OnCollisionStay(Collision collisionInfo)
    {
        // Check if the objects are still in contact

        if (collisionInfo.gameObject.CompareTag("Player"))
        {

            Debug.Log("Objects are in constant contact");

            if (alreadyAttacked == false)
            {
                player.TakeDamage(damageAmount);
                hitSound.Play();
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision");
        //Debug.Log("Tag of collided object: " + other.gameObject.tag);


        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(2);
        }
    }



}
