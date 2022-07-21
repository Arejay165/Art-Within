using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EmojiManager : MonoBehaviour
{
    // Start is called before the first frame update
    //GameObject playerRef;
    public static EmojiManager instance;
    public List<Sprite> email;
    public List<Sprite> emojis;
    public List<Sprite> caraEmotionBust;
    public List<Sprite> celfEmotionBust;
    public List<Sprite> ruskatEmotionBust;
    public List<AnimationClip> emojiClips; 
   // BustSpeakerCharacter speakerCharacter;
    // PlayerInteractUI interactUI;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        return;
    }

    public void ShowSprite(Image speakerIcon)
    {
        speakerIcon.transform.parent.gameObject.SetActive(true);
        speakerIcon.color = new Color(255, 255, 255, 255);
    }

    public void ResetSprite(List<Image> speakerIcon)
    {
        for(int i = 0; i < speakerIcon.Count; i++)
        {
            
            speakerIcon[i].transform.parent.gameObject.SetActive(false);
            speakerIcon[i].color = new Color(0, 0, 0, 0);
        }
       
    }

    public void EmojiIntializer(Conversation dialogue, Image speakerIcon)
    {
        
        ShowSprite(speakerIcon);
        switch (dialogue.emote)
        {
            case Emotes.ExclaimationMark:
                speakerIcon.sprite = emojis[0];
                break;
            case Emotes.QuestionMark:
                speakerIcon.sprite = emojis[1];
                break;
            case Emotes.Heart:
                speakerIcon.sprite = emojis[2];
                break;
            case Emotes.Lightbulb:
                speakerIcon.sprite = emojis[3];
                break;
            case Emotes.Thinking:
                speakerIcon.sprite = emojis[4];
                break;

            case Emotes.Happy:
                speakerIcon.sprite = emojis[5];
                break;
            case Emotes.Yaw:
                speakerIcon.sprite = emojis[6];
                break;
            default:
                break;
        }
    }

    public void CaraBustIntializer(Conversation dialogue, Image speakerIcon)
    {
        switch (dialogue.bustEmotions)
        {
            case SpeakerBustEmotions.Happy:
                speakerIcon.sprite = caraEmotionBust[0];
                break;

            case SpeakerBustEmotions.Angry:
                speakerIcon.sprite = caraEmotionBust[2];
                break;

            case SpeakerBustEmotions.Sad:
                speakerIcon.sprite = caraEmotionBust[1];
                break;
        }
    }

    public void CelfBustInitializer(Conversation dialogue, Image speakerIcon)
    {
        switch (dialogue.bustEmotions)
        {
            case SpeakerBustEmotions.Happy:
                speakerIcon.sprite = celfEmotionBust[0];
               // speakerIcon.sprite = null;
                break;

            case SpeakerBustEmotions.Sad:
                speakerIcon.sprite = celfEmotionBust[1];
               // speakerIcon.sprite = null;
                break;
        }
    }

    public void RuskatBustInitializer(Conversation dialogue, Image speakerIcon)
    {
        switch (dialogue.bustEmotions)
        {
            case SpeakerBustEmotions.Happy:
                speakerIcon.sprite = ruskatEmotionBust[0];
                // speakerIcon.sprite = null;
                break;

        }
    }

    public void UnknownBustInitializer(Conversation dialogue, Image speakerIcon)
    {
        switch (dialogue.bustEmotions)
        {
            default:
                speakerIcon.sprite = email[0];
                break;
        }
      
    }

    public void CharacterBustInitializer(Conversation dialogue, Image speakerIcon)
    {
        switch (dialogue.speakerCharacter)
        {
            case BustSpeakerCharacter.Cara:
                CaraBustIntializer(dialogue,speakerIcon);
                break;

            case BustSpeakerCharacter.Celf:
                CelfBustInitializer(dialogue, speakerIcon);
                break;

            case BustSpeakerCharacter.Ruskat:
                RuskatBustInitializer(dialogue, speakerIcon);
                break;
            case BustSpeakerCharacter.Unknown:
                UnknownBustInitializer(dialogue, speakerIcon);
                break;



        }
    }

    public void AnimationEmojiInitializer(Conversation dialogue, Animator speakerEmojiAnim)
    {
        // ShowSprite(speakerIcon);
        speakerEmojiAnim.gameObject.SetActive(true);
        switch (dialogue.emote)
        {
            case Emotes.ExclaimationMark:
                speakerEmojiAnim.Play(emojiClips[0].name);
                break;
            case Emotes.QuestionMark:
                speakerEmojiAnim.Play(emojiClips[1].name);
                break;
            case Emotes.Heart:
                speakerEmojiAnim.Play(emojiClips[2].name);
                break;
            case Emotes.Lightbulb:
                speakerEmojiAnim.Play(emojiClips[3].name);
                break;
            case Emotes.Thinking:
                speakerEmojiAnim.Play(emojiClips[4].name);
                break;

            case Emotes.Happy:
                speakerEmojiAnim.Play(emojiClips[5].name);
                break;
            case Emotes.Yaw:
                speakerEmojiAnim.Play(emojiClips[6].name);
                break;
            default:
                break;
        }
    }
}
