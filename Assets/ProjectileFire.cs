using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;
    
    public GameObject InitializeProjectile(GameObject target)
    {
        GameObject spawnedProjectile = Instantiate(projectile, firePoint.position, Quaternion.identity);

        float AngleRad = Mathf.Atan2(target.transform.position.y - spawnedProjectile.transform.position.y,
        target.transform.position.x - spawnedProjectile.transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        spawnedProjectile.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, AngleDeg);

        return spawnedProjectile;
    }
}
