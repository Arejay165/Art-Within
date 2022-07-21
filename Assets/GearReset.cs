using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearReset : MonoBehaviour
{
    InteractableObj interactable;
    [SerializeField] GameObject Lever;
    GearManager gearManager;


    void Start()
    {
        interactable = GetComponentInChildren<InteractableObj>();
        interactable.OnInteract += OnInteract;
        interactable.DEBUG += () => gearManager.OnPuzzleComplete?.Invoke();
        interactable.ApplyUI();
        gearManager = GetComponentInParent<GearManager>();
    }

    void OnInteract()
    {
        if (gearManager.complete)
            return;

        Lever.transform.rotation = Quaternion.Euler(Lever.transform.rotation.eulerAngles.x * -1, Lever.transform.rotation.eulerAngles.y,
            Lever.transform.rotation.eulerAngles.z);

        gearManager.stopped = false;
        foreach(GameObject gear in gearManager.Gears)
        {
            gear.GetComponent<Gear>().ResetGear();
        }
        foreach (GameObject gear in gearManager.Switches)
        {
            gear.GetComponent<GearSwitch>().ResetSwitch();
        }
    }
}