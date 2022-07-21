using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLandingState : EnemyBaseFSM
{
    HealthComponent enemyHealth;
    FlyingEnemyAI flyingEnemyBase;
    Animator anim;

    bool landed = false;
    bool fly = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent.isStopped = true;
        agent.ResetPath();
        combatAI.stateLocked = true;

        landed = false;
        fly = false;

        enemyHealth = animator.GetComponentInParent<HealthComponent>();
        anim = animator;

        flyingEnemyBase = animator.GetComponentInParent<FlyingEnemyAI>();

        flyingEnemyBase.OnFlyHeightReached += OnFlyHeightReached;
        flyingEnemyBase.OnLandHeightReached += OnLandHeightReached;

        flyingEnemyBase.Land();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyBase.timerDone && !fly)
        {
            flyingEnemyBase.Fly();
            enemyHealth.invulnerable = true;
            fly = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("land", false);
    }
    void OnLandHeightReached()
    {
        enemyBase.StartFleeTimer();
        landed = true;
        enemyHealth.invulnerable = false;
    }

    void OnFlyHeightReached()
    {
        combatAI.stateLocked = false;

        enemyBase.timerDone = false;
        agent.isStopped = true;

        if (combatAI.currentTarget == null)
        {
            anim.SetBool("idle", true);
        }
        else
        {
            anim.SetTrigger("chase");
        }
        anim.SetBool("land", false);
    }
    
}
