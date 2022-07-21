using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class RuskatConvo : MonoBehaviour
{

    public bool isTalking = true;

    public RsukatDialogue[] rsukatDialogue;

    bool inRange;
    int counter;
    [HideInInspector]
    public int dialogueIndex;
    

    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    GameObject panelImage;

    public GameObject newTalk;
    Movement_CC playerMovement;

    GameObject player;

    bool askRuksat;

    Ruskat ruskat;
    bool canNowTalk;



   
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<Movement_CC>();
        ruskat = GetComponentInParent<Ruskat>();
        //Bruteforce solution to typewriter
        ruskat.unTalkable += Untalkable;
        ruskat.Talkable += Talkable;
        player.GetComponent<PlayerController>().Painting += Untalkable;
        player.GetComponent<PlayerController>().NotPainting += Talkable;
   //     canNowTalk = false;
    }

    private void OnEnable()
    {
       // Debug.Log("Enable");
        StartMessage();
    }

    private void OnDisable()
    {
       // Debug.Log("Disable");
        canNowTalk = false;
        EndConvo();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !askRuksat)
        {
            askRuksat = true;
        }

        if (canNowTalk)
        {
          //  Debug.Log("Can now talk");
            Dialogue();
        }
        else
        {
           // Debug.Log("Can't talk");
        }

        if (text.gameObject.activeInHierarchy)
        {
            // isTalking = true;
            inRange = true;
            playerMovement.canMove = false;
            playerMovement.Stop();
            askRuksat = true;
        }
        else
        {
            counter = 0;
            isTalking = false;

        }

    }

    //Bruteforce solution can't display the counter - 1 message unlike in AIConvo
    void Dialogue()
    {
        if (askRuksat)
        {
            if (!isTalking)
            {
                // Make this into Press Q instead of P, can't make it work atm
                if (Input.GetMouseButtonDown(1))
                {

                    if (newTalk.activeInHierarchy)
                    {
                        newTalk.SetActive(false);
                    }
                    text.gameObject.SetActive(true);
                    panelImage.SetActive(true);
                    //Display all texts immediately instead of typewriting
                    if (!DialogueManager.instance.isFinishTalking)
                    {
                        ForceShowMessage();
                        DialogueManager.instance.isFinishTalking = true;
                        return;
                    }
                    else
                    {
                        PlayMessage();
                    }
                }

            }
        }
           
    }


    //Bruteforce start solution for typewriter to work on one press
    IEnumerator NextMessage()
    {
        askRuksat = false;
        isTalking = true;
        PlayMessage();
        yield return new WaitForSeconds(0.2f);
        isTalking = false;
        DialogueManager.instance.isFinishTalking = true;
    }


    //Text and counter logic
    void PlayMessage()
    {
        StopAllCoroutines();
        inRange = true;
        panelImage.SetActive(true);
        text.text = "";
        if (counter < rsukatDialogue[dialogueIndex].conversations.Count)
        {
          
            text.text = rsukatDialogue[dialogueIndex].conversations[counter].messages;

            StartCoroutine(DialogueManager.instance.TypeSentence(text.text, text, 
                rsukatDialogue[dialogueIndex].conversations[counter].soundType));
        
        }
        else
        {
            EndConvo();
        }

        counter++;
    }

    void EndConvo()
    {
        //Debug.Log("Endcovo");
        StopAllCoroutines();
        inRange = false;
        isTalking = false;
        playerMovement.canMove = true;
       // canNowTalk = false;
        text.gameObject.SetActive(false);
        panelImage.SetActive(false);

    }

    //Bruteforce solution can't display the counter - 1 message unlike in AIConvo
    void ForceShowMessage()
    {
       /// Debug.Log("Force");
        int i = counter - 1;
        if(i == -1)
        {
            i = 0;
            text.text = rsukatDialogue[dialogueIndex].conversations[0].messages;

        }
        else
        {
            text.text = rsukatDialogue[dialogueIndex].conversations[i].messages;

        }
         StopAllCoroutines();
    }


    public void Untalkable()
    {
      ///  Debug.Log("Untalkable");
       // talkable?.Invoke();
        this.enabled = false;
    }

    public void Talkable()
    {
       // Debug.Log("Talkable");
        this.enabled = true;
    }

    public IEnumerator Message()
    {
        yield return new WaitForSeconds(10f);
        StartCoroutine(NextMessage());
    }

    public void StartMessage()
    {
        //StartCoroutine(Message());
        canNowTalk = true;
       
    }

    public void EditScaleSizeConvo(float size)
    {
        panelImage.gameObject.GetComponent<RectTransform>().localScale = new Vector3(size, size, size);
    }

    public void LargeSizePosition()
    {
        panelImage.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-12.76f, 7f, 1.07f);
    }

    public void NormalSizePosition()
    {
        panelImage.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-2.83f, 3.19f, 1.07f);
        panelImage.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.05113089f, 0.05113089f, 0.05113089f);
    }
}
