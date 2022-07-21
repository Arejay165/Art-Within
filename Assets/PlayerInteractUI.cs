using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class PlayerInteractUI : MonoBehaviour
{
    [Header("InteractPromptUI")]
    public Image PlayerActionIcon;
    public Image PlayerEmojiIcon;
    [SerializeField] TextMeshProUGUI playerActionText;

    [Header("PlayerDialogueUI")]

    public GameObject PlayerDialougePanel;
    public TextMeshProUGUI PlayerDialogueText;

    [Header("PlayerInputSprite")]
    [SerializeField] Sprite LMBSprite;
    [SerializeField] Sprite RMBSprite;
    [SerializeField] Image InputIcon;


    public Animator animatorEmote;

    [Header("TransitionUI")]
    public GameObject UITransition;
    //public Action DisableIcon;

    //private void Start()
    //{
    //    DisableIcon += DisableUI;
    //}

    public void SetInputText(string text)
    {
        switch (text)
        {
            case "LMB":
                InputIcon.enabled = true;
                InputIcon.sprite = LMBSprite;
                break;
            case "RMB":
                InputIcon.enabled = true;
                InputIcon.sprite = RMBSprite;
                break;
        }
        playerActionText.text = text;
    }
    public TextMeshProUGUI PlayerActionText
    {
        get { return playerActionText; }
        set
        {
            playerActionText = value;
            Debug.Log("ActionText");
        }
    }

    public string PlayerActionTextString
    {
        get { return playerActionText.text; }
        set
        {
            playerActionText.text = value;

            if (InputIcon == null)
                return;

            switch(playerActionText.text)
            {
                case "LMB":
                    InputIcon.enabled = true;
                    InputIcon.sprite = LMBSprite;
                    break;
                case "RMB":
                    InputIcon.enabled = true;
                    InputIcon.sprite = RMBSprite;
                    break;
            }
        }
    }

    public void DisableUI()
    {
        PlayerActionIcon.gameObject.SetActive(false);
    }
}
