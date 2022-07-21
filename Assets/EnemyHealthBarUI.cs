using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image fill;
    [SerializeField] GameObject sprite;
    HealthComponent healthComponent;
    Enemy enemyBase;
    Quaternion rotation;
    private void Start()
    {
        healthComponent = transform.GetComponentInParent<HealthComponent>();
        enemyBase = transform.GetComponentInParent<Enemy>();

        SetMaxHealth(healthComponent.MaxHP);
        healthComponent.OnDamage += ReduceHP;
        healthComponent.OnZeroHealth += () =>
        {
            slider.value = 0;
            gameObject.SetActive(false);
        };

        healthComponent.OnInvul += () => fill.color = new Color(1, 0.8f, 0);
        healthComponent.OffInvul += () => fill.color = Color.red;
        rotation = transform.rotation;
    }

    public void SetMaxHealth(float hp)
    {
        slider.maxValue = hp;
        slider.value = hp;
    }

    public void ReduceHP(float hp)
    {
        slider.value -= hp;
        enemyBase.sfxSource.PlayOneShot(enemyBase.soundEvent.clips[3]);
    }

    private void LateUpdate()
    {
        transform.rotation = rotation;
    }
}
