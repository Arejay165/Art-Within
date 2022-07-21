using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HomeCanvas : MonoBehaviour
{
    InteractableObj interactable;
    CanvasPaint paintComp;

    PlayerInteractUI playerInteractUI;

    bool painted = false;
    [SerializeField] GameObject UICanvas;
    [SerializeField] GameObject Canvas;

    public UnityEvent OnPainted;
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerInteractUI = player.gameObject.GetComponent<PlayerInteractUI>();

        interactable = GetComponentInChildren<InteractableObj>();
        paintComp = GetComponentInChildren<CanvasPaint>();

        if (interactable)
        {
            interactable.OnInteract += OnInteract;
            interactable.OnOutOfRange += () => UICanvas.SetActive(false);
        }

        if (paintComp)
        {
            paintComp.ApplyUI();
            paintComp.hasInteractable = true;
            paintComp.PaintFilled += () =>
            {
                interactable.ApplyUI();

                OnPainted?.Invoke();
            };
        }
    }

    public void SetPainted()
    {
        painted = true;
        player.GetComponent<PlayerController>().anim.Play("Player_Idle");
        interactable.ApplyUI();
        playerInteractUI.PlayerActionIcon.gameObject.SetActive(true);

        player.GetComponent<Paint>().StopPaintSFX();
    }

    void OnInteract()
    {
        if (painted)
        {
            UICanvas.SetActive(!UICanvas.activeSelf);
            Canvas.SetActive(UICanvas.activeSelf);
            playerInteractUI.PlayerActionIcon.gameObject.SetActive(!UICanvas.activeSelf);
        }
    }
}
