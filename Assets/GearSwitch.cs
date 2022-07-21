using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSwitch : MonoBehaviour
{
    [SerializeField] List<GameObject> Gear;
    [SerializeField] Material ActivatedMaterial;
    [SerializeField] MeshRenderer Cannister;
    InteractableObj interactable;
    GearManager gearManager;
    Material oldMaterial;

    [HideInInspector] public bool activated = false;
    bool hasMove;
    void Start()
    {
        interactable = GetComponentInChildren<InteractableObj>();
        gearManager = GetComponentInParent<GearManager>();
        interactable.OnInteract += OnInteract;

        oldMaterial = Cannister.material;
        interactable.ApplyUI();
    }

    void OnInteract()
    {
        Debug.Log("Interact");

        if (activated && !gearManager.complete)
        {
            //if (!hasMove)
            //{
                
                foreach(GameObject g in Gear)
                {
                    g.GetComponent<Gear>().ActivateGear();
                }
                Cannister.material = ActivatedMaterial;
                gearManager.counter++;
                hasMove = true;
            //}
           
        }
    }

    public void ResetSwitch()
    {
        gearManager.counter = 0;
        Cannister.material = oldMaterial;
        hasMove = false;
    }
}
