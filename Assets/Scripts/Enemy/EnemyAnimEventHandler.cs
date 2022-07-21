using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyAnimEventHandler : MonoBehaviour
{
    AttackComp enemyAttack;
    FlyingEnemyAI flyingEnemy;
    EnemyMovementAI enemyMovementAI;
    Animator animator;
    Enemy enemyBase;

    VisualEffect deathEffect;
    float waitTime;
    private void Start()
    {
        enemyAttack = GetComponentInParent<AttackComp>();
        enemyMovementAI = GetComponentInParent<EnemyMovementAI>();
        animator = GetComponent<Animator>();
        deathEffect = GetComponent<VisualEffect>();
        flyingEnemy = GetComponentInParent<FlyingEnemyAI>();
        enemyBase = GetComponentInParent<Enemy>();
        waitTime = enemyAttack.attackDelay;
    }
    public void Attack()
    {
        enemyAttack.Attack();
    }

    public void PlayWalkSFX()
    {
        enemyBase.sfxSource.PlayOneShot(enemyBase.soundEvent.clips[1]);
    }

    public void AttackWait()
    {
        StartCoroutine(WaitTime());
    }

    public void ProjectileAttack()
    {
        flyingEnemy.FireProjectile();
        enemyBase.sfxSource.PlayOneShot(enemyBase.soundEvent.clips[2]);
    }

    public void PlayDeathVFX()
    {
        deathEffect.Play();
    }

    IEnumerator WaitTime()
    {
        animator.SetFloat("attackTime", 0);
        yield return new WaitForSeconds(waitTime);
        animator.SetFloat("attackTime", 1);
    }
}
