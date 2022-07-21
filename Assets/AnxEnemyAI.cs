using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxEnemyAI : Enemy
{
    protected override void OnDamageTemp(float dmg)
    {
        animator.SetBool("hide", true);
    }
}
