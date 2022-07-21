using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintPickup : Pickup
{
    [SerializeField] float paintAmount;
  
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnPickUp(GameObject taker)
    {
        taker.GetComponent<Paint>().PaintMeter += paintAmount;
        taker.GetComponentInChildren<VFXHandler>().PlayVFX(VFXHandler.VFX.PaintPickup);

    }
}
