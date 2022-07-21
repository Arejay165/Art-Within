using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyPickup : Pickup
{
    public UnityEvent KeyPickedUp;
    protected override void OnPickUp(GameObject taker)
    {
        taker.GetComponent<PlayerController>().KeyPickedUp(GetComponent<SpriteRenderer>().sprite);
        
        KeyPickedUp?.Invoke();
    }
}
