using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi2 : MonoBehaviour
{

    public enum FSMStates
    {
        Patrol,
        Chase,
        Attack,
        Dead
    }

    public GameObject[] wanderPoints;
    Vector3 nextDestination;
    public Animator anim;
    float distanceToPlayer;

    public float enemySpeed = 5;
    public float chaseDistance = 10;
    public float attackDistance = 5;

    public GameObject player;
    public ParticleSystem deadvfx;
    float elapsedTime = 0;

    EnemyHealth enemyHealth;
    int health;

    int currentDestinationIndex = 0;
    Transform deadTransform;
    bool isDead = false;

    public NavMeshAgent agent;

    public Transform enemyEyes;
    public float FOV = 45f;

    public FSMStates currentState;
    // Start is called before the first frame update
    void Awake()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");

        enemyHealth = GetComponent<EnemyHealth>();

        health = enemyHealth.currentHealth;

        Initialize();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        health = enemyHealth.currentHealth;
        switch (currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break;

        }
        elapsedTime += Time.deltaTime;

        if (health <= 0)
        {
            currentState = FSMStates.Dead;
        }
    }

    private void Initialize()
    {
        currentState = FSMStates.Patrol;
        FindnextPoint();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void UpdatePatrolState()
    {
        anim.SetInteger("State", 1);

        agent.stoppingDistance = 0;

        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindnextPoint();
        }
        else if (distanceToPlayer <= chaseDistance && IsPlayerInClearFOV())
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);

        //transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
    }

    void UpdateChaseState()
    {
        anim.SetInteger("State", 1);

        agent.stoppingDistance = attackDistance;

        agent.speed = 5;

        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            FindnextPoint();
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);

        //transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
    }
    void UpdateAttackState()
    {
        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);

        anim.SetInteger("State", 2);
    }
    void UpdateDeadState()
    {
        agent.speed = 0;
        anim.SetInteger("State", 3);
        isDead = true;
        deadTransform = gameObject.transform;

        Destroy(gameObject, 3);
    }

    void FindnextPoint()
    {

        nextDestination = wanderPoints[currentDestinationIndex].transform.position;

        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;

        agent.SetDestination(nextDestination);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }



    private void OnDestroy()
    {
        Instantiate(deadvfx, transform.position, transform.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, chaseDistance);

        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * chaseDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0, FOV * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -FOV * .5f, 0) * frontRayPoint;

        //Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
        //Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.yellow);
        //Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.yellow);
    }

    bool IsPlayerInClearFOV()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.transform.position - enemyEyes.position;

        if (Vector3.Angle(directionToPlayer, enemyEyes.forward) <= FOV)
        {
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }
}
