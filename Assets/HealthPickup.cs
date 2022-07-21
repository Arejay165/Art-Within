using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField] float healthAmount = 5f;
    protected override void OnPickUp(GameObject taker)
    {
        taker.GetComponent<HealthComponent>().AddHealth(healthAmount);
        taker.GetComponentInChildren<VFXHandler>().PlayVFX(VFXHandler.VFX.HealthRegen);
    }
}
