using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTransition : MonoBehaviour
{
    public GameObject ElevatorUI;
    Teleport teleport;
    PromptAction prompt;

    bool animationPlaying = false;
    Coroutine wait;
    Movement_CC movement;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        teleport = GetComponent<Teleport>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement_CC>();
        prompt = GetComponent<PromptAction>();
        audio = GetComponent<AudioSource>();
    }

    public void PlayAnimation()
    {
        if (animationPlaying)
            return;

        animationPlaying = true;
        ElevatorUI.GetComponent<Animator>().SetTrigger("Close");
        audio.Play();
        movement.canMove = false;
        movement.Stop();


        if (wait != null)
            StopAllCoroutines();
        wait = StartCoroutine(WaitTime());
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0.5f);
        teleport.SpawnTeleport();
        prompt.TriggerExit();
        yield return new WaitForSeconds(2f);
        ElevatorUI.GetComponent<Animator>().SetTrigger("Open");
        movement.canMove = true;
        animationPlaying = false;
    }
}
