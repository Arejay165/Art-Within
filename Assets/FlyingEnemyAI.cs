using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlyingEnemyAI : Enemy
{
    ProjectileFire projectileComp;
    [SerializeField] int projectilesBeforeLanding = 5;
    [SerializeField] float landingSpeed = 5f;

    [SerializeField] GameObject Sprite;
    [SerializeField] Transform FlyHeight;
    [SerializeField] Transform LandHeight;

    public Action OnFlyHeightReached;
    public Action OnLandHeightReached;
    EnemyMovementAI movementAI;

    int currProjectileCount;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        projectileComp = GetComponent<ProjectileFire>();
        movementAI = GetComponent<EnemyMovementAI>();
        GetComponent<HealthComponent>().invulnerable = true;
    }

    public void Fly()
    {
        StartCoroutine(MoveSprite(FlyHeight, OnFlyHeightReached));
    }
    
    public void Land()
    {
        StartCoroutine(MoveSprite(LandHeight, OnLandHeightReached));
    }

    IEnumerator MoveSprite(Transform destinationTransform, Action EventFired)
    {
        while (Vector3.Distance(Sprite.transform.localPosition, destinationTransform.localPosition) > 0.1f)
        {
            Sprite.transform.localPosition = Vector3.MoveTowards(Sprite.transform.localPosition, destinationTransform.localPosition, 
                landingSpeed * Time.deltaTime);
            yield return null;
        }
        EventFired?.Invoke();
    }

    public void FireProjectile()
    {
        if (enemyCombatAI.currentTarget == null)
            return;

        movementAI.LookAtTarget(enemyCombatAI.currentTarget.transform.position);

        GameObject spawnedProjectile = projectileComp.InitializeProjectile(enemyCombatAI.currentTarget);
        Projectile projectileData = spawnedProjectile.GetComponent<Projectile>();

        //Put in Z offset for the projectile to match the player for better aim.
        //spawnedProjectile.transform.position = 
        //    new Vector3(spawnedProjectile.transform.position.x, spawnedProjectile.transform.position.y,
        //    spawnedProjectile.transform.position.z);

        projectileData.Init(enemyCombatAI.currentTarget.transform.position);
        projectileData.OnProjectileHit += OnProjectileHit;

        currProjectileCount++;
        if (projectilesBeforeLanding == currProjectileCount)
        {
            currProjectileCount = 0;
            animator.SetBool("land", true);
        }
    }

    void OnProjectileHit()
    {
        if (enemyCombatAI.currentTarget != null)
        {
            if (enemyCombatAI.currentTarget.TryGetComponent(out HealthComponent healthComp))
            {
                healthComp.DamagerData = this.gameObject;
                healthComp.ReduceHealth(enemyData.Damage);
            }
        }
    }
}
