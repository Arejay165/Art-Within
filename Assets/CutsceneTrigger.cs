using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject CutsceneManagerObj;
    Prompt prompt;
    CutsceneManager cutsceneManager;

    private void Start()
    {
        prompt = CutsceneManagerObj.GetComponentInChildren<Prompt>(true);
        cutsceneManager = CutsceneManagerObj.GetComponentInChildren<CutsceneManager>(true);

        cutsceneManager.OnCutSceneEnd.AddListener(() => Destroy(this.gameObject));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Movement_CC>().Stop();
            if (cutsceneManager)
            {
                prompt.StartConvo();
            }
        }
    }
}
