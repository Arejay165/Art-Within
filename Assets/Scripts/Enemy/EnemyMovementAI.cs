using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementAI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;

    //Movement AI Variables
    Vector3 originalPosition;

    [SerializeField] float wanderRadius = 4f; //So that the animal wont wander too far from its original spawn point
    [SerializeField] float minIdleTime = 3f;
    [SerializeField] float maxIdleTime = 5f;
    [SerializeField] float animatationOffset = 1;

    public float facingDirX = 1;
    bool isFlipped = false;
    Vector3 gizmoPosition;

    public bool GoingBackToSpawn;
    public bool OutOfBounds;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        facingDirX = 1;
        originalPosition = transform.position;
    }

    public void StartWanderTimer()
    {
        StartCoroutine(WanderTimer());
    }
    IEnumerator WanderTimer()
    {
        float time = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(time);

        agent.isStopped = false;
        MoveTo(GetRandomPointInNavMesh());
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 newPosition = transform.position + gizmoPosition;
        Gizmos.DrawWireSphere(newPosition, wanderRadius);
    }

    public Vector3 GetRandomPointInNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;

        //origin of the sphere
        randomDirection += originalPosition;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, agent.areaMask))
        {
            return hit.position;
        }
        else
        {
            //Debug.LogError(agent.name + " " + "SAMPLE POSITION ERROR");
            return transform.position;
        }
    }

    public void LookAtTarget(Vector3 location)
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        Vector3 dir = (transform.position - location).normalized;
        float dot = Vector3.Dot(Vector3.right, transform.InverseTransformPoint(location));

        if (dot > 0)
        {
            facingDirX = 1;
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            isFlipped = !isFlipped;
        }
        if (isFlipped)
        {
            facingDirX = -1;
        }

        return;

        //Old code
        if (dot > 0 && isFlipped)
        {
            facingDirX = 1;
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            isFlipped = false;
        }
        else if (dot < 0 && !isFlipped)
        {
            facingDirX = -1;
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            isFlipped = true;
        }

        //if (transform.position.x > location.x && isFlipped)
        //{
        //    facingDirX = 1;
        //    transform.localScale = flipped;
        //    transform.Rotate(0, 180, 0);
        //    isFlipped = false;
        //    Debug.Log("flip");
        //}

        //if (transform.position.x < location.x && !isFlipped)
        //{
        //    facingDirX = -1;
        //    transform.localScale = flipped;
        //    transform.Rotate(0, 180, 0);
        //    isFlipped = true;
        //}
    }

    public void MoveTo(Vector3 location)
    {
        agent.ResetPath();
        Vector3 endPoint = new Vector3(location.x, transform.position.y, location.z);
        NavMeshPath path = new NavMeshPath();

        NavMeshHit hit;

        if (!NavMesh.SamplePosition(endPoint, out hit, wanderRadius, agent.areaMask))
            return;

        agent.CalculatePath(hit.position, path);

        if (path.status != NavMeshPathStatus.PathInvalid)
        {
            agent.SetDestination(endPoint);
            LookAtTarget(location);
        }
    }

    public void GoBackToSpawn()
    {
        if (GetComponent<EnemyCombatAI>().stateLocked)
            return;

        animator.SetTrigger("return");
        MoveTo(originalPosition);
        GoingBackToSpawn = true;
    }
}
