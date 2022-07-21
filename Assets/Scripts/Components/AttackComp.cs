using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComp : MonoBehaviour
{
   // PlayerAnimation playerAnimation;

    public LayerMask targetLayer;
    public float attackRadius;
    public Transform attackPoint;
    public float damage;
    public float attackDelay;
    public AudioSource source;


    private void Start()
    {
       //playerAnimation = GetComponent<PlayerAnimation>();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
    public void Attack()
    {
        Collider[] hit = Physics.OverlapSphere(attackPoint.position, attackRadius, targetLayer);
        
        

        foreach (Collider enemy in hit)
        {
            if (enemy.TryGetComponent(out HealthComponent healthComp))
            {
                healthComp.DamagerData = this.gameObject;
                healthComp.ReduceHealth(damage);
            }
        }

        //if (hit.Length != 0 && hit[0].TryGetComponent(out HealthComponent healthComp))
        //{
        //    healthComp.ReduceHealth(damage);
        //}
    }

}
