using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class Paintable : InteractableObj
{
    [SerializeField] float currentPaint = 0;
    [SerializeField] float requiredPaint = 5;
    [SerializeField] float paintSpeed = 1;

    public bool paintable;

    [Header("PaintShader Properties")]
    [SerializeField] float minCutOff;
    [SerializeField] float maxCutOff;

    public Action<GameObject> OnPaint;
    public Action PaintFilled;

    public Action PaintStart;
    public Action PaintStop;

    [Header("Paint Graphics Properties")]
    [SerializeField] GameObject paintableMeshObj;
    [SerializeField] GameObject unpaintedMesh;
    [SerializeField] List<MeshRenderer> meshRend;
    [SerializeField] Image PaintProgressUI;
    [SerializeField] Image PaintProgressUI2;

    [HideInInspector] public bool hasInteractable = false;

    List<MaterialPropertyBlock> propBlock = new List<MaterialPropertyBlock>();

    Paint playerPaintRef;

//    Collider collider;
    // Update is called once per frame

    protected override void Start()
    {
        base.Start();

        paintableMeshObj.GetComponentsInChildren(meshRend);

        for (int i = 0; i < meshRend.Count; i++)
        {
            propBlock.Add(new MaterialPropertyBlock());
            meshRend[i].SetPropertyBlock(propBlock[i]);
        }

        playerPaintRef = player.GetComponent<Paint>();

       // collider = gameObject.GetComponent<Collider>();
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
                if (playerPaintRef.PaintMeter > 0)
                {
                    playerPaintRef.PlayPaintSFX();
                    PaintStart?.Invoke();

                    playerController.Painting?.Invoke();
                }
                else
                {
                    playerPaintRef.OnPaintChanged?.Invoke(0);
                }
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
                playerController.anim.Play("Player_Idle");
                playerPaintRef.StopPaintSFX();
                playerMovement.canMove = true;
                PaintStop?.Invoke();

                playerController.NotPainting?.Invoke();
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
        if (playerPaintRef.PaintMeter > 0)
        {
            playerPaintRef.PaintMeter -= paintSpeed * Time.deltaTime;
            currentPaint += paintSpeed * Time.deltaTime;

            if(PaintProgressUI != null)
            {
                PaintProgressUI.enabled = true;
                PaintProgressUI.fillAmount = currentPaint / requiredPaint;
            }
           
            if(PaintProgressUI2 != null)
            {
                PaintProgressUI2.enabled = true;
                PaintProgressUI2.fillAmount = currentPaint / requiredPaint;
            }
          

            playerController.anim.Play("Player_Wave_Paint_F");

            playerMovement.canMove = false;
            playerMovement.Stop();
        }
        else
        {
            playerController.anim.Play("Player_Idle");
            playerController.NotPainting?.Invoke();
            playerPaintRef.StopPaintSFX();
            PaintStop?.Invoke();
            playerMovement.canMove = true;
            return;
        }

        for (int i = 0; i < meshRend.Count; i++)
        {
            meshRend[i].GetPropertyBlock(propBlock[i]);
            //The "-" is the minimum range, the multiply is the max range.
            propBlock[i].SetFloat("_Cutoff_Height", (currentPaint / requiredPaint * maxCutOff) + minCutOff);
            meshRend[i].SetPropertyBlock(propBlock[i]);
        }


        //If complete
        if (currentPaint >= requiredPaint)
        {
            PaintFilled?.Invoke();
            //  collider.enabled = false;
            interactCol.enabled = false;

            if (PaintProgressUI != null)
            {
                PaintProgressUI.enabled = false;
            }
           

            if(PaintProgressUI2 != null)
            {
                PaintProgressUI2.enabled = false;
            }
      
            //playerInteractUI.PlayerActionIcon.gameObject.SetActive(false);
            if (unpaintedMesh)
                unpaintedMesh.SetActive(false);

            playerController.anim.Play("Player_Idle");
            playerPaintRef.StopPaintSFX();
            PaintStop?.Invoke();
            playerController.NotPainting?.Invoke();
            //   this.enabled = false; // disabling the paintable script, fixing the player can still paint even when its full. 
            gameObject.SetActive(false);

        }
    }

    private void OnDisable()
    {
        //interactPrompt.SetActive(false);
        if (playerRef != null)
        {
            playerController.anim.Play("Player_Idle");
            playerPaintRef.StopPaintSFX();
            playerMovement.canMove = true;
            PaintStop?.Invoke();
            playerController.NotPainting?.Invoke();
            if (!hasInteractable)
                playerInteractUI.PlayerActionIcon.gameObject.SetActive(false);
        }
    }
}
