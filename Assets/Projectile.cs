using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    [SerializeField] float ProjectileSpeed;
    bool startFiring;
    public Action OnProjectileHit;
    public void Init(Vector3 target)
    {
        StartCoroutine(Move(target));
        StartCoroutine(bulletLifespan());
    }

    IEnumerator Move(Vector3 target)
    {
        Vector3 dir = (target - transform.position).normalized;

        while (true)
        {

            //transform.position += transform.right * ProjectileSpeed * Time.deltaTime;

            transform.position += dir * ProjectileSpeed * Time.deltaTime;

            //transform.position = Vector3.Lerp(transform.position, target, ProjectileSpeed * Time.deltaTime);

            //transform.Translate(Vector3.Normalize(transform.position - target) * ProjectileSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnProjectileHit?.Invoke();
            StopAllCoroutines();
            Destroy(this.gameObject);
        }
        //else if(!other.CompareTag("Enemy"))
        //{
        //    Destroy(this.gameObject);
        //}
    }

    IEnumerator bulletLifespan()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
