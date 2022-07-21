using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    protected GameObject playerRef;
    public TextMeshProUGUI interactableText;
    public Sprite interactableSprite;
    protected GameObject interActablePanel;
    protected PlayerInteractUI interactUI;
    public bool isPlayerPrompt;
    public Sprite actionSprite;
    public SpriteRenderer questSprite;

    protected virtual void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        interactUI = playerRef.GetComponent<PlayerInteractUI>();

        if(!isPlayerPrompt)
        {
            if(transform.parent != null)
            {
                interactableText = transform.parent.GetComponentInChildren<TextMeshProUGUI>(true);
                interactableSprite = transform.parent.GetComponentInChildren<Image>(true).sprite;
                interActablePanel = transform.parent.gameObject;

            }

        }
        else 
        {
            interactableSprite = interactUI.PlayerActionIcon.sprite;
            interactableText = interactUI.PlayerActionText;
            interActablePanel = interactUI.PlayerActionIcon.gameObject;

            
        }

        
    }
    protected virtual void Start()
    {
        //interactableText = interactUI.PlayerDialougeText;
        //interactableSprite = interactUI.PlayerActionIcon.sprite;
    }

    public void ApplyActionSprite()
    {
        if (actionSprite)
        {
            interactUI.PlayerActionIcon.sprite = actionSprite;
        }
    }
}
