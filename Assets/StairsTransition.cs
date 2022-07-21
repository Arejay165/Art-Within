using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StairsTransition : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] GameObject Fade;
    Teleport teleport;
    GameObject player;
    bool teleporting = false;

    PromptAction promptAction;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        teleport = GetComponent<Teleport>();
        promptAction = GetComponent<PromptAction>();
    }

    public void PlayTransition()
    {
        if (teleporting)
            return;

        teleporting = true;

        StopAllCoroutines();
        StartCoroutine(Transition());   
    }
    IEnumerator Transition()
    {
        player.GetComponent<Movement_CC>().Stop();
        Fade.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
        yield return new WaitForSeconds(0.5f);

        audioSource.Play();
        teleport.SpawnTeleport();
        promptAction.TriggerExit();
        yield return new WaitForSeconds(0.8f);
        Fade.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
        player.GetComponent<Movement_CC>().canMove = true;
        teleporting = false;
    }
}
