using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]

public class EnemyBehaviour : MonoBehaviour
{
    public enum EnemyStates
    {
        Idle,
        Patrol,
        Suspicious,
        FindPlayer
    }

    public EnemyStates currentSate;

    [SerializeField]
    Transform[] targets;
    [SerializeField]
    GameObject player;
    [SerializeField]
    float catchedDistance = 2f;
    [SerializeField]
    float lostDistance = 10f;
    private int destinationTarget = 0;
    private NavMeshAgent agent; 
    private Animator animator;
    private static readonly int animState = Animator.StringToHash("AnimState");
    private int currentAnim;

    private void Start()
    {
        agent= GetComponent<NavMeshAgent>(); 
        agent.autoBraking = false;
        animator = GetComponent<Animator>();

        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
        }
    }

    private void Update()
    {
        currentAnim = animator.GetInteger(animState);

        switch (currentSate)
        {
            case EnemyStates.Idle:
                Idle();
                break;

            case EnemyStates.Patrol:
                Patrolling();
                break;

            case EnemyStates.Suspicious:
                animator.SetInteger(animState, 2);
                agent.isStopped = true;
                break;

            case EnemyStates.FindPlayer:
                MoveToPlayer();
                break;

            default:
                currentSate = EnemyStates.Patrol;
                break;

        }
    }

    void Idle()
    {
        agent.isStopped = true;
        animator.SetInteger(animState, 0);

        if (Vector3.Distance(transform.position, player.transform.position) >= lostDistance)
        {
            currentSate = EnemyStates.Patrol;
        }
    }

    void Patrolling()
    {
        agent.isStopped=false;
        animator.SetInteger(animState, 1);

        if (!agent.pathPending && agent.remainingDistance < 0.01f)
        {
            agent.SetDestination(targets[destinationTarget].position);
            destinationTarget = (destinationTarget + 1) % targets.Length;
        }
    }

    void MoveToPlayer()
    {
        animator.SetInteger(animState, 3);
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);


        if (Vector3.Distance(transform.position, player.transform.position) <= catchedDistance)
        {
            currentSate = EnemyStates.Idle;
        }

        if (Vector3.Distance(transform.position, player.transform.position) >= lostDistance)
        {
            currentSate = EnemyStates.Patrol;
        }
    }

    private void OnDrawGizmos()
    {
        for (var i = 1; i < targets.Length; i++)
        {
            Debug.DrawLine(targets[i - 1].transform.position, targets[i].transform.position);
        }
    }
}
