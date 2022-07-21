using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class AIConvo : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Dialogue Properties")]

    public Dialogue[] dialogues;
    private int counter;
    private int dialogueCounter;

    [Header("Initialization Properties")]
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject npcPanel;

    // public Animator selfAnim;
    Animator playerAnim;
    private Movement_CC playerMovement;
    PlayerController playerController;

    private int playerID;
    private int selfID;

    bool hasDoneTalkingOnce;

    [HideInInspector]
    public List<string> messages;

    public bool canStopPlayer;
    public bool willDestory;

    [HideInInspector]
    public List<Conversation> conversation;

    public UnityEvent OnConversationEnd;

    //public NPCAnimation npCAnimation;

    GameObject player;

    private NPCCutsceneUIController cutsceneController;

    public Action<int> changeIndex;

    public Action unTalkable;
    public Action talkable;

    RuskatConvo ruskatConvo;

    GameObject ruskat;
    // RuskatConvo ruskatConvo;

    // private NPCUIController pCUIController;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<Movement_CC>();

        playerController = player.GetComponent<PlayerController>();

        playerAnim = player.GetComponent<Animator>();

      
        

    }

    private void OnEnable()
    {
        cutsceneController = gameObject.GetComponentInParent<NPCCutsceneUIController>();

        // cutsceneController.SpeakerTalk += NextMessage;
        ruskat = GameObject.FindGameObjectWithTag("Ruskat");

        if (ruskat != null)
            ruskatConvo = ruskat.GetComponent<RuskatConvo>();


        //   pCUIController = gameObject.GetComponentInParent<NPCUIController>();
        // pCUIController.panel.SetActive(true);
        //Debug.Log("Was being enable");
        selfID = 1;
        // playerID = 0;
        playerID = DialogueManager.instance.playerID;
        MessageInitializer();

      //  unTalkable?.Invoke();

        if(ruskatConvo != null)
        {
            ruskatConvo.Untalkable();
        }

        if (!canStopPlayer)
        {
            text.text = conversation[counter].messages.ToString();

            counter = 0;
        }
        else
        {
            text.gameObject.SetActive(false);
            DialogueManager.instance.narratorText.text = conversation[counter].messages.ToString();
            //playerMovement.MovementToggle();
            playerMovement.Stop();
            playerAnim.Play("Player_Idle");
        }


        if (conversation[counter].hasBust)
        {
            SpeakerBust();
        }
        else
        {
            if (cutsceneController.pCUIController.personaPanel != null)
            cutsceneController.pCUIController.personaPanel.transform.parent.gameObject.SetActive(false);

        }
        //if (willDestory)
        //{
        //    SpeakerSpoke();
        //}
        // playerInput++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!DialogueManager.instance.isFinishTalking)
            {
                ForceShowMessage();
                DialogueManager.instance.isFinishTalking = true;
                return;
            }
            else
            {
                SpeakerSpoke();
            }

        }
    }

    void NextMessage()
    {
        text.text = "";
        text.text = conversation[counter].messages.ToString();
       // Debug.Log(counter + "Normal counter");
        switch (conversation[counter].speaker)
        {
            case Speaker.Player:
                DialogueManager.instance.PlayerMessage(conversation[counter].messages.ToString());
                DialogueManager.instance.PlayerText.text = conversation[counter].messages.ToString();
                break;
            case Speaker.NPC:
                cutsceneController.dialoguePanels[conversation[counter].npcIndex].SetActive(true);
                cutsceneController.texts[conversation[counter].npcIndex].gameObject.SetActive(true);

                cutsceneController.texts[conversation[counter].npcIndex].text = conversation[counter].messages.ToString();
                // cutsceneController.texts[conversation[counter].npcIndex].text = conversation[counter].messages.ToString();
                break;
            case Speaker.Narrator:
                DialogueManager.instance.DisablePlayerMessage();

                DialogueManager.instance.NarratorMessage(conversation[counter].messages);
                //   DialogueManager.instance.narratorText.text = conversation[counter].messages.ToString();
                break;
            case Speaker.AllNPC:
                for (int i = 1; i < cutsceneController.actors.Length; i++)
                {

                    cutsceneController.texts[i].text = conversation[counter].messages.ToString();
                }
                break;

            default:
                break;
        }

    }

    void ForceShowMessage()
    {
        StopAllCoroutines();
      //  Debug.Log(counter - 1 + "Minus - 1");

        switch (conversation[counter - 1].speaker)
        {
            case Speaker.Player:
                DialogueManager.instance.PlayerText.text = conversation[counter - 1].messages.ToString();
                break;
            case Speaker.NPC:

                cutsceneController.dialoguePanels[conversation[counter - 1].npcIndex].SetActive(true);
                cutsceneController.texts[conversation[counter - 1].npcIndex].text = conversation[counter - 1].messages.ToString();
                break;
            case Speaker.Narrator:
                DialogueManager.instance.narratorText.text = conversation[counter - 1].messages.ToString();
                break;
            case Speaker.AllNPC:

                for (int i = 1; i < cutsceneController.actors.Length; i++)
                {
                    cutsceneController.dialoguePanels[i].SetActive(true);
                    cutsceneController.texts[i].gameObject.SetActive(true);
                    cutsceneController.texts[i].text = "";
                    cutsceneController.texts[i].text = conversation[counter - 1].groupMessage[i].ToString();
                }

                break;

            default:
                break;
        }
    }


    void SpeakerBust()
    {
         if (cutsceneController.pCUIController.bustIcon != null)
        {
            cutsceneController.pCUIController.personaPanel.transform.parent.gameObject.SetActive(true);
            DialogueManager.instance.DisablePlayerMessage();
            DialogueManager.instance.DisableNarratorMessage();
            cutsceneController.pCUIController.speakerName.text = conversation[counter].speakerName;
            EmojiManager.instance.CharacterBustInitializer(conversation[counter], cutsceneController.bustImage[conversation[counter].npcIndex]);
        }
      
    }

    void EndConvo()
    {
        StopAllCoroutines();
        //playerInput = 0;
      //  ruskatConvo?.Talkable();
        OnConversationEnd?.Invoke();
       // EmojiManager.instance.ResetSprite(cutsceneController.emojiIcon);
        DialogueManager.instance.DisablePlayerMessage();
        npcPanel.SetActive(false);
        DialogueManager.instance.DisableNarratorMessage();
        playerAnim.Play("Player_Idle");


        if (ruskatConvo != null)
        {
            ruskatConvo.Talkable();
        }
        player.GetComponent<PlayerInteractUI>().PlayerEmojiIcon.gameObject.SetActive(false);

     //   player.GetComponent<PlayerInteractUI>().PlayerEmojiIcon = null;
        if (cutsceneController != null)
        {
            for (int i = 0; i < cutsceneController.anim.Count; i++)
            {
                cutsceneController.anim[i].ResetToIdle();
                if (cutsceneController.emojiIcon[i] != null)
                    cutsceneController.emojiIcon[i].transform.parent.gameObject.SetActive(false);
            }
        }
        

        if (canStopPlayer)
        {
            //playerController.canAttack = false;
           // DialogueManager.instance.DisableNarratorMessage();

        }

        if (willDestory)
        {
            Destroy(transform.parent.gameObject);
        }
       // talkable?.Invoke();
        gameObject.SetActive(false);


    }


    private void OnDisable()
    {
       // Debug.Log("Was being disabled");
        if (dialogues[dialogueCounter].RepeatLinesAt != 0 && dialogues[dialogueCounter].afterDialouge.Count == 0)
        {
            counter = dialogues[dialogueCounter].RepeatLinesAt;
        }
        else
        {
            counter = 0;
        }

        //dialogues[dialogueCounter].afterDialouge.Count

        if (dialogues[dialogueCounter].afterDialouge.Count != 0)
        {
            hasDoneTalkingOnce = true;
        }
        MessageInitializer();
        if (canStopPlayer)
        {
            //playerMovement.MovementToggle();
            playerMovement.canMove = true;
        }

       // talkable?.Invoke();
    }
    public void SpeakerSpoke()
    {
        //messages.Count
    //    EmojiManager.instance.ResetSprite(cutsceneController.emojiIcon);
        if(cutsceneController != null)
        cutsceneController.DisableNPCUI();

      

        if (counter < messages.Count)
        {
            playerAnim.Play("Player_Idle");
            // DialogueManager.instance.NextMessage(conversation[counter].messages,text);
            NextMessage();
          

            if (conversation[counter].hasBust)
            {
                changeIndex?.Invoke(conversation[counter].cutsceneIndex);
                SpeakerBust();
            }
            else
            {
                if (cutsceneController.pCUIController.personaPanel != null)
                cutsceneController.pCUIController.personaPanel.transform.parent.gameObject.SetActive(false);

            }

            for (int i = 0; i < cutsceneController.anim.Count; i++)
            {
                cutsceneController.anim[i].ResetToIdle();
                if(cutsceneController.emojiIcon[i] != null)
                cutsceneController.emojiIcon[i].transform.parent.gameObject.SetActive(false);
            }


            player.GetComponent<PlayerInteractUI>().PlayerEmojiIcon.gameObject.SetActive(false);

           
            SpeakReaction();
            SpeakerEmotes();
        }
        else
        {
            EndConvo();

            return;
        }

        StopAllCoroutines();


        switch (conversation[counter].speaker)
        {
            case Speaker.Player:
                cutsceneController.DisableNPCUI();
                DialogueManager.instance.DisableNarratorMessage();
                DialogueManager.instance.PlayerMessage(conversation[counter].messages.ToString());
                StartCoroutine(DialogueManager.instance.TypeSentence(DialogueManager.instance.PlayerText.text, DialogueManager.instance.PlayerText,
                   conversation[counter].soundType));

                break;

            case Speaker.NPC:

                DialogueManager.instance.DisablePlayerMessage();
                DialogueManager.instance.DisableNarratorMessage();

                cutsceneController.dialoguePanels[conversation[counter].npcIndex].SetActive(true);
                cutsceneController.texts[conversation[counter].npcIndex].gameObject.SetActive(true);

                cutsceneController.texts[conversation[counter].npcIndex].text = conversation[counter].messages.ToString();

                StartCoroutine(DialogueManager.instance.TypeSentence(cutsceneController.texts
                    [conversation[counter].npcIndex].text, cutsceneController.texts
                    [conversation[counter].npcIndex],
                          conversation[counter].soundType));
                break;

            case Speaker.Narrator:

                cutsceneController.DisableNPCUI();
               
                DialogueManager.instance.DisablePlayerMessage();
               
                DialogueManager.instance.NarratorMessage(conversation[counter].messages);
                StartCoroutine(DialogueManager.instance.TypeSentence(DialogueManager.instance.narratorText.text, DialogueManager.instance.narratorText,
                 conversation[counter].soundType));
                break;

            case Speaker.AllNPC:
                if (conversation[counter].npcMaxSpeakers > 1)
                {
                    DialogueManager.instance.DisablePlayerMessage();
                    DialogueManager.instance.DisableNarratorMessage();
                    for (int i = 1; i < conversation[counter].groupMessage.Count; i++)
                    {
                        cutsceneController.dialoguePanels[i].SetActive(true);
                        cutsceneController.texts[i].gameObject.SetActive(true);

                        cutsceneController.texts[i].text = conversation[counter].groupMessage[i].ToString();

                        StartCoroutine(DialogueManager.instance.TypeSentence(cutsceneController.texts
                        [i].text, cutsceneController.texts[i], conversation[counter].soundType));

                    }
                }

                break;

            default:
                break;
        }

        counter++;

    }
    public void SpeakReaction()
    {
        //dialogues[dialogueCounter].conversations[counter].animationName.Count
        if (conversation[counter].animationName.Count != 0)
        {
            //dialogues[dialogueCounter].conversations[counter].speakerAnimation
            switch (conversation[counter].speakerAnimation)
            {
                case SpeakerAnimation.PlayerOnly:
                    playerAnim.Play(conversation[counter].animationName[playerID]);
                    
                    break;

                case SpeakerAnimation.NPCOnly:
      
                    cutsceneController.anim[conversation[counter].npcIndex].anim.
                        Play(conversation[counter].animationName[0]);
                 

                    break;

                case SpeakerAnimation.Both:
                    if (playerAnim != null && cutsceneController.anim[conversation[counter].npcIndex] != null)
                    {
                        playerAnim.Play(conversation[counter].animationName[playerID]);
                        cutsceneController.anim[conversation[counter].npcIndex].anim.
                            Play(conversation[counter].animationName[1]);
                      //  EmojiManager.instance.EmojiIntializer(conversation[counter], cutsceneController.emojiIcon[counter]);

                    }
                    break;

                case SpeakerAnimation.AllNPC:

                    if (conversation[counter].npcMaxSpeakers > 1)
                    {
                        for (int i = 1; i < conversation[counter].animationName.Count; i++)
                        {
                            cutsceneController.anim[i].anim.Play(conversation[counter].animationName[i]);
                         //   EmojiManager.instance.EmojiIntializer(conversation[counter], cutsceneController.emojiIcon[conversation[counter].npcIndex]);

                        }
                    }
                    break;
                default:
                    break;

                  
            }
        }
    }
    public void MessageInitializer()
    {
        messages.Clear();
        conversation.Clear();
        if (!hasDoneTalkingOnce)
        {
            for (int i = 0; i < dialogues[dialogueCounter].conversations.Count; i++)
            {
                messages.Add(dialogues[dialogueCounter].conversations[i].messages);
                conversation.Add(dialogues[dialogueCounter].conversations[i]);
            }
        }
        else
        {
            for (int i = 0; i < dialogues[dialogueCounter].afterDialouge.Count; i++)
            {
                messages.Add(dialogues[dialogueCounter].afterDialouge[i].messages);
                conversation.Add(dialogues[dialogueCounter].afterDialouge[i]);
            }
        }
    }

    public void SpeakerEmotes()
    {
       
        switch (conversation[counter].speakerEmoji)
        {
            case SpeakerEmoji.Player:
                
                for(int i = 0; i < cutsceneController.emojiAnim.Count; i++)
                {
                    cutsceneController.emojiAnim[i].gameObject.SetActive(false);
                }
                player.GetComponent<PlayerInteractUI>().PlayerEmojiIcon.gameObject.SetActive(true);
                EmojiManager.instance.AnimationEmojiInitializer(conversation[counter], player.GetComponent<PlayerInteractUI>().animatorEmote);
                


               // EmojiManager.instance.EmojiIntializer(conversation[counter], player.GetComponent<PlayerInteractUI>().PlayerEmojiIcon);
                break;

            case SpeakerEmoji.NPC:
                //   EmojiManager.instance.EmojiIntializer(conversation[counter], cutsceneController.emojiIcon[conversation[counter].npcIndex]);

                EmojiManager.instance.AnimationEmojiInitializer(conversation[counter], cutsceneController.emojiAnim[conversation[counter].npcIndex - 1]);
                break;

            case SpeakerEmoji.None:
                for (int i = 0; i < cutsceneController.anim.Count; i++)
                {
                    cutsceneController.anim[i].ResetToIdle();
                    if (cutsceneController.emojiIcon[i] != null)
                    {
                        cutsceneController.emojiIcon[i].transform.parent.gameObject.SetActive(false);
                    }
                }

                for (int i = 0; i < cutsceneController.emojiAnim.Count; i++)
                {
                    cutsceneController.emojiAnim[i].gameObject.SetActive(false);
                }
                player.GetComponent<PlayerInteractUI>().PlayerEmojiIcon.gameObject.SetActive(false);
                break;

            case SpeakerEmoji.ALLNPC:
                if (conversation[counter].npcMaxSpeakers > 1)
                {
                    for (int i = 1; i < conversation[counter].groupMessage.Count; i++)
                    {

                        //  EmojiManager.instance.EmojiIntializer(conversation[counter], cutsceneController.emojiIcon[i]);
                        EmojiManager.instance.AnimationEmojiInitializer(conversation[counter], cutsceneController.emojiAnim[i]);
                    }
                }

                break;

            default:
                break;
        }
    }
}
