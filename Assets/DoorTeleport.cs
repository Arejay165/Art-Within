using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    public Transform TeleportPoint1;
    public Transform TeleportPoint2;

    PlayerController player;
    Movement_CC playerMovement;

    bool internalBuffer = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerController>();
            playerMovement = other.gameObject.GetComponent<Movement_CC>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

        }
    }

    private void OnEnable()
    {
        StartCoroutine(buffer());
    }

    IEnumerator buffer()
    {
        yield return new WaitForEndOfFrame();
        internalBuffer = true;
    }

    public void TeleportPlayer()
    {
        if (player == null && internalBuffer)
            return;

        if (Vector3.Distance(player.transform.position, TeleportPoint1.position) >
            Vector3.Distance(player.transform.position, TeleportPoint2.position))
        {
            player.TeleportPlayer(TeleportPoint1.position);
        }
        else
        {
            player.TeleportPlayer(TeleportPoint2.position);
        }
        StartCoroutine(stopPlayer());
    }

    private void OnDisable()
    {
        if (player != null)
        {
            StopAllCoroutines();
            player.enabled = true;
            playerMovement.canMove = true;
        }
    }

    IEnumerator stopPlayer()
    {
        playerMovement.Stop();
        player.enabled = false;
        yield return new WaitForSeconds(1f);
        playerMovement.canMove = true;
        player.enabled = true;
    }
}
