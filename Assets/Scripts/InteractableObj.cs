using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class InteractableObj : MonoBehaviour
{
    protected CapsuleCollider interactCol;
    public Sprite InteractIcon;
    protected bool inRange = false;
    protected GameObject playerRef;

    public Action OnInteract;
    public Action DEBUG;
    public Action OnOutOfRange;

    protected PlayerController playerController;
    protected Movement_CC playerMovement;
    protected PlayerInteractUI playerInteractUI;

    protected GameObject player;

    [SerializeField] bool willApplyUI = false;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.gameObject.GetComponent<PlayerController>();
        playerMovement = player.gameObject.GetComponent<Movement_CC>();
        playerInteractUI = player.gameObject.GetComponent<PlayerInteractUI>();
    }
    protected virtual void Start()
    {
        interactCol = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (willApplyUI)
            {
                playerInteractUI.PlayerActionIcon.sprite = InteractIcon;
                playerInteractUI.PlayerActionTextString = "LMB";
            }

            //interactPrompt.SetActive(true);
            playerInteractUI.PlayerActionIcon.gameObject.SetActive(true);
            playerRef = other.gameObject;


            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //interactPrompt.SetActive(false);
            playerInteractUI.PlayerActionIcon.gameObject.SetActive(false);
            inRange = false;
            playerMovement.canMove = true;
            OnOutOfRange?.Invoke();
            //playerRef = null;
        }

    }

    public void ApplyUI()
    {
        willApplyUI = true;
        playerInteractUI.PlayerActionIcon.sprite = InteractIcon;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && inRange)
        {
            OnInteract?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.P) && inRange)
        {
            DEBUG?.Invoke();
        }
    }
}
