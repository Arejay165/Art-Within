using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] List<DetectionRadius> Checkpoints;
    // Start is called before the first frame update
    void Start()
    {
        foreach(DetectionRadius c in Checkpoints)
        {
            c.OnDetectionEnter += SetCheckpoint;
        }
    }

    void SetCheckpoint(GameObject sender, GameObject player)
    {
        player.GetComponent<PlayerController>().CurrentCheckpoint = sender.transform.position;
    }
}
