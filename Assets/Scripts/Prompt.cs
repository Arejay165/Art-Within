using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Prompt : Interactable
{
    // Start is called before the first frame update

    public string messages;


    public bool hasCutsceneDialogue;

    public bool autoplay;

  
    Movement_CC playerMovementScript;
    
    [SerializeField] GameObject dialogueCanvas;

    [SerializeField]
    public bool promptMessage;
    AIConvo aIConvo;
    GameObject parent;
    
    protected bool hasEnter;

    bool firstEncounter = true;


    public Action InteractAction;
    

    protected override void Start()
    {
        base.Start();
        playerMovementScript = playerRef.GetComponent<Movement_CC>();
        if (transform.parent != null)
        {
            parent = transform.parent.gameObject;
            aIConvo = transform.parent.GetComponentInChildren<AIConvo>(true);
            if (aIConvo != null)
                aIConvo.OnConversationEnd.AddListener(OnConvoEnd);
        }
       
       
    }

    void OnConvoEnd()
    {
        interActablePanel.SetActive(true);
        interactableText.transform.gameObject.SetActive(true);

        interactUI.PlayerActionTextString = messages;
    }

    public void EnterEnable()
    {
        firstEncounter = true;
        hasEnter = true;
    }

    private void Update()
    {
        if (isPlayerPrompt)
        {
            if (hasEnter)
            {
                // ApplyActionSprite();
                //interactableText.text = messages;
                Interact();
       
            }
           
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasEnter = true;
            interactUI.PlayerActionTextString = messages;
            ApplyActionSprite();

            if (promptMessage)
            {
                interActablePanel.SetActive(true);
                interactableText.transform.gameObject.SetActive(true);
                //interactableText.text = messages;
                interactUI.PlayerActionTextString = messages;
                if (questSprite != null)
                    questSprite.gameObject.SetActive(false);
            }

            if (autoplay)
            {
                other.gameObject.GetComponent<PlayerController>().input = Vector2.zero;

                if(dialogueCanvas != null)
                dialogueCanvas.SetActive(true);

                if (questSprite != null)
                    questSprite.gameObject.SetActive(false);

                //interactableText.text = "";
                interactUI.PlayerActionTextString = "";
                StartCoroutine(startAIConvo());
            }
            else
            {
                //if (questSprite != null)
                //    questSprite.gameObject.SetActive(false);

                interActablePanel.SetActive(true);
                interactableText.gameObject.SetActive(true);
                interactableText.text = messages;
            }

        }
    }

    IEnumerator startAIConvo()
    {
        yield return new WaitForSeconds(0.1f);
        aIConvo.SpeakerSpoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasEnter = false;
            if (hasCutsceneDialogue)
            {
            
                dialogueCanvas.gameObject.SetActive(false);
            }
            //if (!willHide)
            //{

            //}

              if (transform.parent != null)
            {
                interactableText.transform.gameObject.SetActive(false);
                interActablePanel.SetActive(false);
                //interactableText.text = "";
                interactUI.PlayerActionTextString = "";
            }
        
            //  interactableText.gameObject.SetActive(false);

        }
    }


    private void OnDisable()
    {
        if(transform.parent != null && interactableText != null && interActablePanel != null)
        {
            interactableText.transform.gameObject.SetActive(false);
            interActablePanel.SetActive(false);
        }

        if (interactUI != null)
            interactUI.PlayerActionTextString = "";

        if (questSprite != null)
            questSprite.gameObject.SetActive(false);
    }


    public void StartConvo()
    {
        hasEnter = false;
        playerRef.gameObject.GetComponent<PlayerController>().input = Vector2.zero;
        dialogueCanvas.SetActive(true);


        if (questSprite != null)
        {
            questSprite.gameObject.SetActive(false);
            //if (!questSprite.gameObject.activeInHierarchy)
            //{
            //    questSprite.gameObject.SetActive(false);
            //}

        }


        //interactableText.text = "";

        if (aIConvo != null)
        {
            InteractAction?.Invoke();
            StartCoroutine(startAIConvo());
        }
    }

    public void Interact()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //interactableText.text = "";
            StartConvo();
               
        }
    }
}

