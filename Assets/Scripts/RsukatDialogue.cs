using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ruskat Dialogue", menuName = "New Ruskat Dialogue")]
public class RsukatDialogue : ScriptableObject
{
   // public string messages;
    public List<Conversation> conversations;
}
