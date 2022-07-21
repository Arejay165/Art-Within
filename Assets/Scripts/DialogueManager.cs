using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static DialogueManager instance;
    //[HideInInspector]
   // public GameObject[] npcs;

    public GameObject playerDialoguePanel;
    public TextMeshProUGUI PlayerText;

    public GameObject narratorDialoguePanel;
    public TextMeshProUGUI narratorText;

    private AudioSource typeSound;
    public float typingSpeed;

    [HideInInspector]
    public int playerID;

    public bool isFinishTalking;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }

        //if(npcs != null)
      //  npcs = GameObject.FindGameObjectsWithTag("Peeps");

        typeSound = gameObject.GetComponent<AudioSource>();

        // typingSpeed = 0.03f;
        playerID = 0;
    }


    public void PlayerMessage(string message)
    {
        playerDialoguePanel.SetActive(true);
        PlayerText.text = message;

    }

    public void DisablePlayerMessage()
    {
        playerDialoguePanel.SetActive(false);
        PlayerText.text = "";
    }

    public void NarratorMessage(string message)
    {
        narratorDialoguePanel.SetActive(true);
        narratorText.text = message;
    }

    public void DisableNarratorMessage()
    {
        narratorDialoguePanel.SetActive(false);
        PlayerText.text = "";
    }

    public IEnumerator TypeSentence(string sentence, TextMeshProUGUI textTempt , AudioClip soundType)
    {

        textTempt.text = "";
        typeSound.clip = soundType;
       
        foreach (char letter in sentence.ToCharArray())
        {
            textTempt.text += letter;
           // Debug.Log(sentence);
            //Debug.Log(textTempt.text);
            yield return new WaitForSecondsRealtime(typingSpeed);
            typeSound.Play();
            isFinishTalking = false;
           // Debug.Log("isTalking"+ isFinishTalking);
        }
        //ForceStopMessage(sentence, textTempt);
        typeSound.Stop();
        isFinishTalking = true;
        // Debug.Log("isFinished" + isFinishTalking);
    }

    public void NextMessage(string message, TextMeshProUGUI npcText)
    {
        npcText.text = message;
        PlayerText.text = message;
        narratorText.text = message;


    }

    public void ForceStopMessage(string message, TextMeshProUGUI npcText)
    {
        StopAllCoroutines();
        PlayerText.text = message;
        narratorText.text = message;
         npcText.text = message;
        //for (int i = 0; i < npcText.Count; i++)
        //{
        //    npcText[i].text = message;
        //}
    }
}
