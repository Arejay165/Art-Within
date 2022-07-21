using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class PromptAction : Prompt
{
    // Start is called before the first frame update

    public UnityEvent inputPressed;
    
    CharacterController playerController;


    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();

        playerController = playerRef.GetComponent<CharacterController>();
    }


    private void Update()
    {
        if (hasEnter)
        {

            if (Input.GetMouseButtonDown(0) && playerRef != null)
            {
              //  Debug.Log(playerRef.name);
                // inputPressed?.Invoke();
                playerController.enabled = false;
                inputPressed?.Invoke();
                playerController.enabled = true;

                hasEnter = false;

            
                if (interActablePanel != null)
                interActablePanel.SetActive(false);

            }
       
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasEnter = true;
            ApplyActionSprite();

            if(interActablePanel != null)
            interActablePanel.SetActive(true);

            interactUI.SetInputText(messages);
        }
    }

    public void TriggerExit()
    {
        hasEnter = false;
        // teleport.enabled = false;
        // teleport.triggerable = false;
        interActablePanel.SetActive(false);

        interactUI.SetInputText(messages);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasEnter = false;
            // teleport.enabled = false;
            // teleport.triggerable = false;
            interActablePanel.SetActive(false);

            interactUI.SetInputText(messages);

        }
    }
}
