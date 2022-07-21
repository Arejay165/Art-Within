using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Stats")]
    public float MaxHp;
    public float Damage;
    public float AttackDelay;

    [Header("Loot Properties")]
    public GameObject[] Drops;
    public int MinDropCount;
    public int MaxDropCount;
}
