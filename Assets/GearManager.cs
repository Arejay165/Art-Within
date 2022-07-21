using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GearManager : MonoBehaviour
{
    public List<GameObject> Gears;
    public List<GameObject> Switches;
    [SerializeField] GameObject LastGear;

    public List<GameObject> requiredGears;
    public bool complete = false;

    public UnityEvent OnPuzzleComplete;
    public bool stopped = false;
    bool Activated;
    [HideInInspector]
    public int counter = 0;
    [SerializeField] AudioSource audioSource;
    void Start()
    {
        foreach(GameObject gear in Gears)
        {
            gear.GetComponent<Gear>().OnGearJammed += StopGears;
            gear.GetComponent<Gear>().OnGearTurn += Complete;
            //  gear.GetComponent<Gear>().OnGearTurn += RequireGearChecker;
        }

         //LastGear.GetComponent<Gear>().OnGearTurn += Complete;
       // LastGear.GetComponent<Gear>().OnGearTurn += RequireGearChecker;
    }

    public void ActivatePuzzle()
    {
        foreach(GameObject gear in Gears)
        {
            gear.GetComponent<Gear>().powered = true;
            gear.GetComponent<Gear>().SetGlowingMaterial();
        }
        foreach (GameObject s in Switches)
        {
            s.GetComponent<GearSwitch>().activated = true;
        }

        foreach (GameObject gears in requiredGears)
        {
            gears.GetComponent<Gear>().Rotating = false;
        }

        audioSource.Play();
    }
    void StopGears()
    {
        //if (stopped)
        //    return;
        stopped = true;
        counter = 0;
        foreach (GameObject gear in Gears)
        {
            //gear.GetComponent<Gear>().Jammed = true;
            gear.GetComponent<Gear>().Rotating = false;
            gear.GetComponent<Gear>().SetGlowingMaterial();
        }
    }
    void Complete()
    {
        int gearCount = 0;
        foreach (GameObject gear in requiredGears)
        {
            if (gear.GetComponent<Gear>().Rotating == true)
            {
                gearCount++;
            }
        }

        if (gearCount == requiredGears.Count)
        {
            Debug.Log("Puzzle Complete");
            complete = true;
            OnPuzzleComplete?.Invoke();
        }
        else
            Debug.Log("not complete");
    }

  public void RequireGearChecker()
    {
        Debug.Log("All gears rotating");
        Debug.Log(counter);
        for(int i = 0; i < requiredGears.Count; i++)
        {
            if (requiredGears[i].GetComponent<Gear>().Rotating == true)
            {
                counter++;
            }
        }

        if(counter >= requiredGears.Count)
        {
            Complete();
        }
    }
}
