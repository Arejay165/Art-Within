using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyCombatAI : MonoBehaviour
{
    DetectionRadius detectionRadius;
    Animator animator;
    //Combat Variables will move to sep script
    public GameObject currentTarget;
    public float minChaseDistance = 1f;
    public bool stateLocked = false;
    EnemyMovementAI enemyMovementAI;
    PlayerController playerRef;

    bool inDetectionRadius = false;

    private void Awake()
    {
        detectionRadius = GetComponentInChildren<DetectionRadius>();
        animator = GetComponentInChildren<Animator>();
        enemyMovementAI = GetComponent<EnemyMovementAI>();
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void Start()
    {
        detectionRadius.OnDetectionEnter += OnDetectionEnter;
        detectionRadius.OnDetectionExit += OnDetectionExit;
        playerRef.InBattleStance += ChasePlayerInBattleStance;
    }
    void ChasePlayerInBattleStance()
    {
        if (inDetectionRadius && !stateLocked)
        {
            //fuck it
            currentTarget = playerRef.gameObject;

            animator.SetBool("idle", false);
            animator.SetTrigger("chase");
        }
    }
    public void OnDetectionEnter(GameObject sender, GameObject target)
    {
        inDetectionRadius = true;
        if (stateLocked || enemyMovementAI.OutOfBounds || !playerRef.battleStance)
            return;

        currentTarget = target;

        //Chase
        animator.SetBool("idle", false);
        animator.SetTrigger("chase");
    }

    public void OnDetectionExit(GameObject sender, GameObject target)
    {
        inDetectionRadius = false;
        if (currentTarget == target)
            currentTarget = null;

        if (stateLocked || enemyMovementAI.OutOfBounds)
            return;


        animator.SetBool("idle", true);
        GetComponentInParent<NavMeshAgent>().ResetPath();
        GetComponentInParent<NavMeshAgent>().isStopped = true;
    }
}
