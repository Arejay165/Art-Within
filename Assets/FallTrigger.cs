using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    public float damage = 5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthComponent>().ReduceHealth(damage, false);
            other.GetComponent<PlayerController>().TeleportPlayer(
                other.GetComponent<PlayerController>().CurrentCheckpoint);
        }
    }
}
