using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHideState : EnemyBaseFSM
{
    HealthComponent enemyHealth;
    Vector3 fleeDirection;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent.isStopped = false;
        combatAI.stateLocked = true;
        enemyBase.StartFleeTimer();
        enemyHealth = animator.GetComponentInParent<HealthComponent>();
        enemyHealth.invulnerable = true;
        animator.SetBool("idle", false);

        if (combatAI.currentTarget != null)
            fleeDirection = animator.transform.position - combatAI.currentTarget.transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (combatAI.currentTarget != null)
            fleeDirection = animator.transform.position - combatAI.currentTarget.transform.position;

        Vector3 runTo = animator.transform.position + fleeDirection;
        agent.SetDestination(runTo);

        if (enemyBase.timerDone)
        {
            enemyBase.timerDone = false;
            agent.ResetPath();
            agent.isStopped = true;

            if (combatAI.currentTarget == null)
            {
                animator.SetBool("idle", true);
            }
            else
            {
                animator.SetTrigger("chase");
            }
            animator.SetBool("hide", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        combatAI.stateLocked = false;
        //animator.SetBool("hide", false);
        enemyHealth.invulnerable = false;
    }
}
