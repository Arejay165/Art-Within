using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform spawnPoint;
    public PlayerController playerController;
    public bool triggerable;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggerable)
            return;
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Teleport");
            // LevelManager.instance.TransferPlayerLocation(spawnPoint);
            SpawnTeleport(spawnPoint);
            //   other.gameObject.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);
        }
    }

    public void SpawnTeleport()
    {
        playerController.TeleportPlayer(spawnPoint.position);

        // Debug.Log(spawnPoint.localPosition);
        //LevelManager.instance.TransferPlayerLocation(spawnPoint);
    }

    public void SpawnTeleport(Transform spawnPoint)
    {
        playerController.TeleportPlayer(spawnPoint.position);
       
       // Debug.Log(spawnPoint.localPosition);
        //LevelManager.instance.TransferPlayerLocation(spawnPoint);
    }

}
