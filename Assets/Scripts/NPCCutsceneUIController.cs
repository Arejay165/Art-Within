using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class NPCCutsceneUIController : MonoBehaviour
{
    // Start is called before the first frame update

    //public int actors; 
    public GameObject[] actors;
    [HideInInspector]
    public List<NPCAnimation> anim;
    [HideInInspector]
    public List<GameObject> dialoguePanels;
    [HideInInspector]
    public List<TextMeshProUGUI> texts;
    [HideInInspector]
    public List<Image> emojiIcon;


    public List<Animator> emojiAnim;


    public NPCUIController pCUIController;
    [HideInInspector]
    public List<Image> bustImage;

    //public static Action<Dialogue> dialogueTalk;
    // public AIConvo aIConvo;
   // public Action SpeakerTalk;
   // public Dialogue dialogue;
  //  private List<GameObject> npcs;
    void Start()
    {
        Initialization();
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Initialization()
    {

      //  aIConvo.dialogues[0] += dialogue?.Invoke();
        foreach (GameObject npc in actors)
        {
            //npcs.Add(npc);
            if(npc.GetComponent<NPCUIController>() != null)
            {
                anim.Add(npc.gameObject.GetComponent<NPCUIController>().nPCAnimation);
                pCUIController = actors[0].gameObject.GetComponent<NPCUIController>();
            }
            
            dialoguePanels.Add(npc.GetComponent<NPCUIController>().panel);
            texts.Add(npc.GetComponent<NPCUIController>().text);

            emojiIcon.Add(npc.GetComponent<NPCUIController>().speakerIcon);

            if (npc.GetComponent<NPCUIController>().bustIcon != null)
            {
                bustImage.Add(npc.GetComponent<NPCUIController>().bustIcon);
            }
            
        }
    }

    //Expand this to accomodate two or many speakers
    public void NPCTalk(string message, int actorIndex, AudioClip soundType)
    {
        dialoguePanels[actorIndex].SetActive(true);
        texts[actorIndex].gameObject.SetActive(true);
        DialogueManager.instance.DisablePlayerMessage();
        DialogueManager.instance.DisableNarratorMessage();
       
        texts[actorIndex].text = message;
        StartCoroutine(DialogueManager.instance.TypeSentence(texts[actorIndex].text, texts[actorIndex],
                  soundType));
    }

    public void DisableNPCUI()
    {
        texts[0].text = "";
        for (int i = 1; i < actors.Length; i++)
        {
            dialoguePanels[i].SetActive(false);
            //if (hasSeparateDP)
            //{
            //    dialoguePanels[i].SetActive(false);
            //}
            //else
            //{
            //    dialoguePanels[0].SetActive(true);
            //    break;
            //}

            texts[i].text = "";
          //  texts[i].gameObject.SetActive(false);
        }
    }

    public void PlayerTalk(string message, AudioClip soundType)
    {
        DisableNPCUI();
        DialogueManager.instance.DisableNarratorMessage();

        DialogueManager.instance.PlayerMessage(message);

        Debug.Log(message);

        StartCoroutine(DialogueManager.instance.TypeSentence(DialogueManager.instance.PlayerText.text, DialogueManager.instance.PlayerText,
                   soundType));
    }

    public void NarratorTalk(string message, AudioClip soundType)
    {
        DisableNPCUI();
        DialogueManager.instance.DisablePlayerMessage();
        DialogueManager.instance.NarratorMessage(message);
        StartCoroutine(DialogueManager.instance.TypeSentence(DialogueManager.instance.narratorText.text, DialogueManager.instance.narratorText,
         soundType));

    }


    public void CharacterReactions(int counter)
    {
        //switch (dialogue.conversations[counter].speakerAnimation)
        //{
        //    case SpeakerAnimation.PlayerOnly:

        //        break;

        //    case SpeakerAnimation.NPCOnly:
        //        //anim[]
        //        break;
        //    case SpeakerAnimation.Both:

        //        break;

        //    case SpeakerAnimation.AllNPC:

        //        break;
        //}
    }

    public void EmoteReaction(int counter)
    {
        //switch (dialogue.conversations[counter].speakerEmoji)
        //{
        //    case SpeakerEmoji.Player:

        //        break;

        //    case SpeakerEmoji.NPC:
        //        //anim[]
        //        break;
        //    case SpeakerEmoji.ALLNPC:

        //        break;
        //}
    }

    
}
