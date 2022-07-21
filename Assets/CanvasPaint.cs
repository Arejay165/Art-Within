using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering.HighDefinition;

public class CanvasPaint : InteractableObj
{
    [SerializeField] float currentPaint = 0;
    [SerializeField] float requiredPaint = 5;
    [SerializeField] float paintSpeed = 1;

    public bool paintable;
    [SerializeField] DecalProjector Decal;
    public Action<GameObject> OnPaint;
    public Action PaintFilled;

    [HideInInspector] public bool hasInteractable = false;

    MaterialPropertyBlock materialProperty;

    //    Collider collider;
    // Update is called once per frame

    protected override void Start()
    {
        base.Start();

        Color temp = Decal.material.GetColor("_BaseColor");
        temp.a = 0;
        Decal.material.SetColor("_BaseColor", temp);

        
    }
    void Update()
    {
        if (playerRef != null && inRange)
        {
            if (playerController.isAttacking)
            {
                return;
            }
            if (Input.GetMouseButtonDown(0) && paintable)
            {
                playerRef.GetComponent<Paint>().PlayPaintSFX();
                playerInteractUI.PlayerActionIcon.gameObject.SetActive(false);
            }
            if (Input.GetMouseButton(0))
            {
                if (paintable)
                {
                    FillPaint();
                }
                else
                {
                    Debug.Log("I can't paint on it");
                    playerController.anim.Play("Player_Idle");
                    playerMovement.canMove = true;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Idle");
                playerController.anim.Play("Player_Idle");
                playerRef.GetComponent<Paint>().StopPaintSFX();
                playerInteractUI.PlayerActionIcon.gameObject.SetActive(true);
                playerMovement.canMove = true;
            }
            //if (Input.GetKey(KeyCode.E))
            //{
            //    if (paintable)
            //        FillPaint();
            //    else if (Input.GetKeyDown(KeyCode.E) && !paintable)
            //    {
            //        Debug.Log("I can't paint on it");
            //        playerController.anim.Play("Player_Idle");
            //    }
            //}
        }

    }

    public void FillPaint()
    {
        if (playerRef.GetComponent<Paint>().PaintMeter > 0)
        {
            playerRef.GetComponent<Paint>().PaintMeter -= paintSpeed * Time.deltaTime;
            currentPaint += paintSpeed * Time.deltaTime;

            playerController.anim.Play("Player_Wave_Paint_F");
            playerMovement.canMove = false;
           // playerMovement.Stop();
        }
        else
        {
            //playerController.anim.Play("Player_Idle");
            playerRef.GetComponent<Paint>().StopPaintSFX();
            //playerMovement.canMove = true;
            return;
        }

        Color temp = Decal.material.GetColor("_BaseColor");
        temp.a = (currentPaint / requiredPaint);
        Decal.material.SetColor("_BaseColor", temp);

        if (currentPaint >= requiredPaint)
        {
            PaintFilled?.Invoke();
            //  collider.enabled = false;
            interactCol.enabled = false;

            //playerController.anim.Play("Player_Idle");
            playerRef.GetComponent<Paint>().StopPaintSFX();
            //   this.enabled = false; // disabling the paintable script, fixing the player can still paint even when its full. 
            gameObject.SetActive(false);

        }
    }

    private void OnDisable()
    {
        //interactPrompt.SetActive(false);
        if (playerRef != null)
        {
            if (!hasInteractable)
                playerInteractUI.PlayerActionIcon.gameObject.SetActive(false);
        }
    }
}
