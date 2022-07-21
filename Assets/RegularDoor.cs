using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularDoor : MonoBehaviour
{
    [SerializeField] GameObject DoorHinge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CloseDoor()
    {
        DoorHinge.transform.localRotation = Quaternion.Euler(0, 121, 0);
    }

    public void OpenDoor()
    {
        DoorHinge.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
