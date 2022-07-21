using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ClockManager : MonoBehaviour
{
    public List<GameObject> ClockPlatforms;
    public List<GameObject> AllPlatorms;
    public GameObject ClockObject;

    public UnityEvent OnPuzzleComplete;

    List<GameObject> shapesActive = new List<GameObject>();

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip correct;
    [SerializeField] AudioClip wrong;

    int progress = 0;
    private void Start()
    {
        ClockObject.GetComponent<ClockController>().OnClockTimerEnd += ResetPlatforms;
        foreach (GameObject platform in AllPlatorms)
        {
            platform.GetComponent<ClockPlatform>().OnPlatformEnter += PlatformEnter;
            platform.GetComponent<ClockPlatform>().OnPlatformLeave += PlatformLeave;
            platform.GetComponent<ClockPlatform>().ShapeActivated += ShapeActive;
        }
    }

    void ShapeActive(GameObject platform)
    {
        //platform.GetComponent<ClockPlatform>().Shape.SetActive(false);

        if (!shapesActive.Contains(platform))
            shapesActive.Add(platform);
    }

    void PlatformEnter(GameObject platform)
    {
        foreach(GameObject shape in shapesActive)
        {
            shape.GetComponent<ClockPlatform>().Shape.SetActive(false);
        }
        if (ClockPlatforms[progress] == platform)
        {   
            ClockPlatform clockPlat = platform.GetComponent<ClockPlatform>();
            clockPlat.ShowNextPlatforms();
            ClockObject.GetComponent<ClockController>().ResetHands();
            
            progress++;
 
            audioSource.PlayOneShot(correct);
            //if (clockPlat.NextPlatforms.Count > 0)
            //{
            //    ClockObject.GetComponent<ClockController>().MoveHandTo(
            //        clockPlat.NextPlatforms[0].GetComponent<ClockPlatform>().ClockTimeRep,
            //        clockPlat.NextPlatforms[1].GetComponent<ClockPlatform>().ClockTimeRep);
            //}

            clockPlat.SpotLight.SetActive(true);
            if (progress == ClockPlatforms.Count)
            {
                Complete();
            }
        }
        else
        {
            audioSource.PlayOneShot(wrong);
            ResetPlatforms();
            platform.GetComponent<ClockPlatform>().SetSpotlightColorRed();
            ClockObject.GetComponent<ClockController>().ResetHands();
        }
    }

    void PlatformLeave(GameObject platform)
    {
        if (progress == 0 && ClockPlatforms[progress] == platform)
        {
            platform.GetComponent<ClockPlatform>().SpotLight.SetActive(true);
            ClockObject.GetComponent<ClockController>().ResetHands();
        }
        if (progress == 0)
            return;
        if (ClockPlatforms[progress - 1] == platform && !complete)
        {
            ClockObject.GetComponent<ClockController>().SetClockTimer(platform.GetComponent<ClockPlatform>().TimeLimit);
        }
    }

    void ResetPlatforms()
    {
        foreach(GameObject platform in AllPlatorms)
        {
            platform.GetComponent<ClockPlatform>().Reset();
        }
        foreach(GameObject shape in shapesActive)
        {
            shape.GetComponent<ClockPlatform>().Shape.SetActive(false);
        }
        shapesActive.Clear();
        ClockObject.GetComponent<ClockController>().ResetHands();
        progress = 0;
    }
    bool complete = false;
    public void Complete()
    {
        foreach(GameObject platform in AllPlatorms)
        {
            platform.GetComponent<ClockPlatform>().PuzzleComplete();
        }
        ClockObject.GetComponent<ClockController>().ResetHands();
        OnPuzzleComplete?.Invoke();
        complete = true;
    }
}
