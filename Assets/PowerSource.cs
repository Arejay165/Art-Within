using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerSource : MonoBehaviour
{
    Paintable paintComp;
    public UnityEvent OnPowerOn;
    public bool hasGearPuzzle;
    // Start is called before the first frame update
    void Start()
    {
        paintComp = GetComponentInChildren<Paintable>();
        paintComp.ApplyUI();
        paintComp.PaintFilled = ActivateGearPuzzle;
    }

    void ActivateGearPuzzle()
    {
        if (hasGearPuzzle)
        {
            GetComponentInParent<GearManager>().ActivatePuzzle();
        }
       
        OnPowerOn?.Invoke();
    }
}
