using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitchManager : MonoBehaviour
{
    [SerializeField] List<CinemachineVirtualCamera> cameras;

    public void SwitchCamera(CinemachineVirtualCamera cam)
    {
        foreach (CinemachineVirtualCamera c in cameras)
        {
            c.Priority = 0;
            c.gameObject.SetActive(false);
            if (c == cam)
            {
                c.Priority = 1;
                c.gameObject.SetActive(true);
            }    
        }    
    }
}
