using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


[System.Serializable]
public enum Speaker
{
    Player,
    NPC,
    Narrator,
    AllNPC
};

public enum SpeakerAnimation
{
    PlayerOnly,
    NPCOnly,
    Both,
    AllNPC
};

[System.Serializable]
public class Conversation
{
  
    public string messages;
    public List<string> groupMessage;
    public AudioClip soundType;
    public Speaker speaker;
    public SpeakerAnimation speakerAnimation;
    public SpeakerEmoji speakerEmoji;
    public string speakerName;
    public BustSpeakerCharacter speakerCharacter;
    public Emotes emote;
    public bool hasBust;
    public int cutsceneIndex;
    public SpeakerBustEmotions bustEmotions;
    public int npcIndex;
    public int npcMaxSpeakers;
    public List<string> animationName;
   

  
}

public enum Emotes
{
    ExclaimationMark,
    QuestionMark,
    Heart,
    Lightbulb,
    Thinking, 
    Happy,
    Yaw
};

public enum BustSpeakerCharacter
{
    Cara,
    Celf,
    Ruskat,
    Unknown
}

public enum SpeakerBustEmotions
{
  //  Default,
    Happy,
    Sad,
    Angry
};

public enum SpeakerEmoji
{
    None,
    Player,
    NPC,
    ALLNPC

}


[CreateAssetMenu(fileName = "Dialogue", menuName = "New Dialogue")]
public class Dialogue : ScriptableObject
{

    public List<Conversation> conversations;
    // public List<> emojis;
  //  public List<Emotes> emote;
    public List<Conversation> afterDialouge;
    public int RepeatLinesAt;
    


    [HideInInspector]
    public List<string> actorTalkerName;

    [HideInInspector]
    public List<Sprite> actorBust;

    [HideInInspector]
    public Ease[] ease;

}
