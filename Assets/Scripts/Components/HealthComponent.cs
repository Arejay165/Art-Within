using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] float maxHP;
    [SerializeField] float currentHp;

    public GameObject DamagerData;
    public Action<float> OnDamage;
    public Action<float> ReduceHealthUI;
    public Action<float> OnRegen;
    public Action<float> OnSetHP;

    public Action OnInvul;
    public Action OffInvul;
    public Action OnZeroHealth;
    public Action InvulHit;

    public UnityEvent OnDeath;
    
    bool invul = false;
    public bool invulnerable
    { 
        get { return invul; }
        set 
        {
            invul = value;

            if (value)
                OnInvul?.Invoke();
            else
                OffInvul?.Invoke();
        } 
    }

    public float MaxHP { get { return maxHP; } }

    public void InitData(float pMaxHp)
    {
        maxHP = pMaxHp;
        currentHp = maxHP;
    }

    public void ReduceHealth(float value, bool tempInvokeEvents = true)
    {
        if (invul)
        {
            InvulHit?.Invoke();
            return;
        }

        currentHp -= Mathf.Clamp(value, 0, maxHP);
        ReduceHealthUI?.Invoke(value);
        //StartCoroutine(DamageAnim());
        if (currentHp <= 0)
        {
            OnZeroHealth?.Invoke();
            OnDeath?.Invoke();
        }
        else if (tempInvokeEvents)
        {
            OnDamage?.Invoke(value);
        }
    }

    public void AddHealth(float value)
    {
        currentHp += Mathf.Clamp(value, 0, maxHP);
        OnRegen?.Invoke(value);
    }

    public void ResetHealth()
    {
        currentHp = maxHP;
        OnSetHP?.Invoke(currentHp);
    }

    //TEMP MOVE TO SEPARATE SCRIPT
    IEnumerator DamageAnim()
    {
        SpriteRenderer rend = GetComponentInChildren<SpriteRenderer>();

        for(int i = 0; i <= 5; i++)
        {
            rend.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.1f);
            rend.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
