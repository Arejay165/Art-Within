using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyChaseState : EnemyBaseFSM
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent.isStopped = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (combatAI.currentTarget == null)
        {
            return;
        }
        if (Vector3.Distance(animator.transform.parent.position, combatAI.currentTarget.transform.position) <= combatAI.minChaseDistance)
        {
            //Switch to Attack when close enough
            movementAI.LookAtTarget(combatAI.currentTarget.transform.position);
            animator.SetTrigger("attack");
            agent.isStopped = true;
        }
        else
            movementAI.MoveTo(combatAI.currentTarget.transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
    }
}
