using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSingleTrigger : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] CameraSwitchManager camManager;
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0);
            camManager.SwitchCamera(cam);
        }
    }
}
