using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStanceTrigger : MonoBehaviour
{
    private PlayerController playerRef;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().EnterBattleStance();
        }


        if (other.gameObject.layer == 7)
        {
            if (other.GetComponent<EnemyMovementAI>() != null)
            other.GetComponent<EnemyMovementAI>().OutOfBounds = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ExitBattleStance();
        }

        if (other.gameObject.layer == 7)
        {
            other.GetComponent<EnemyMovementAI>().OutOfBounds = true;
            other.GetComponent<EnemyMovementAI>().GoBackToSpawn();
        }
    }

    private void OnDisable()
    {
        if (playerRef != null)
            playerRef.ExitBattleStance();
    }
}
