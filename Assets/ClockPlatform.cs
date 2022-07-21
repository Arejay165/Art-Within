using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ClockPlatform : MonoBehaviour
{
    public GameObject SpotLight;
    public GameObject Shape;

    // Start is called before the first frame update

    public Action<GameObject> OnPlatformEnter;
    public Action<GameObject> OnPlatformLeave;
    public Action<GameObject> ShapeActivated;

    public List<GameObject> NextPlatforms;

    public float TimeLimit = 10f;

    Color originalColor;
    bool puzzleComplete = false;
    bool startPlatform = false;

    private void Start()
    {
        startPlatform = SpotLight.activeSelf;
        originalColor = SpotLight.GetComponent<Light>().color;
    }

    public void SetSpotlightColorRed()
    {
        SpotLight.SetActive(true);
        SpotLight.GetComponent<Light>().color = Color.red;
    }

    public void ResetColor()
    {
        SpotLight.GetComponent<Light>().color = Color.yellow;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!puzzleComplete)
                OnPlatformEnter?.Invoke(this.gameObject);
        }
    }

    public void ShowNextPlatforms()
    {
        foreach (GameObject platform in NextPlatforms)
        {
            platform.GetComponent<ClockPlatform>().Shape.SetActive(true);
        }
    }

    public void PuzzleComplete()
    {
        puzzleComplete = true;
        SpotLight.SetActive(puzzleComplete);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpotLight.SetActive(puzzleComplete);
            ResetColor();
            foreach (GameObject platform in NextPlatforms)
            {
                //platform.GetComponent<ClockPlatform>().Shape.SetActive(false);
                ShapeActivated?.Invoke(platform);
            }
            OnPlatformLeave?.Invoke(this.gameObject);
        }
    }

    public void Reset()
    {
        SpotLight.SetActive(startPlatform);
    }
}
