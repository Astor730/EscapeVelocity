using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{

    public enum FSMStates
    {
        Patrol,
        Chase, 
        Attack,
    }

    public GameObject[] wanderPoints;
    Vector3 nextDestination;
    Animator anim;
    float distanceToPlayer;

    public float enemySpeed = 5;
    public float chaseDistance = 10;
    public float attackDistance = 5;

    public GameObject player;
    float elapsedTime = 0;

    public NavMeshAgent agent;
    public Transform enemyEyes;
    public float FOV = 180f;

    int currentDestinationIndex = 0;

    public FSMStates currentState;
    // Start is called before the first frame update
    void Start()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");


        Initialize();

        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        switch(currentState)
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
        }
        elapsedTime += Time.deltaTime;
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
        
        //anim.SetInteger("AnimState", 1);
        agent.stoppingDistance = 0;

        if(Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindnextPoint();
        }
        else if(distanceToPlayer <= chaseDistance && IsPlayerInClearFOV())
        {
            print("Inside Else");
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);
    }

    void UpdateChaseState()
    {
        anim.SetInteger("AnimState", 2);

        agent.stoppingDistance = attackDistance;
        agent.speed = enemySpeed;

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
        agent.speed = enemySpeed + 3;

        anim.SetInteger("AnimState", 3);

    }

    void FindnextPoint()
    {
        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
        print(currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length);

        nextDestination = wanderPoints[currentDestinationIndex].transform.position;

        agent.SetDestination(nextDestination);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        print(Quaternion.LookRotation(directionToTarget));
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);   
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
