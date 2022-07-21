using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseFSM : StateMachineBehaviour
{
    protected EnemyCombatAI combatAI;
    protected EnemyMovementAI movementAI;
    protected NavMeshAgent agent;
    protected Enemy enemyBase;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        combatAI = animator.GetComponentInParent<EnemyCombatAI>();
        movementAI = animator.GetComponentInParent<EnemyMovementAI>();
        agent = animator.GetComponentInParent<NavMeshAgent>();
        enemyBase = animator.GetComponentInParent<Enemy>();
        agent.isStopped = false;
    }
}
