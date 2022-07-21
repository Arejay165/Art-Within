using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;

public class Ruskat : MonoBehaviour
{
    [SerializeField] Transform goal;

    [Header("Hover Attributes")]
    [SerializeField] float WaveAmplitude;
    [SerializeField] float RuskatSpeed = 1;

    SpriteRenderer sprite;
    GameObject playerRef;

    [SerializeField] VisualEffectAsset returnVFX;
    [SerializeField] VisualEffectAsset deployVFX;
    VisualEffect visualEffect;

    public Action unTalkable;
    public Action Talkable;
    //RuskatConvo ruskatConvo;
    // Start is called before the first frame update



    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        visualEffect = GetComponent<VisualEffect>();

        //ruskatConvo = GetComponentInChildren<RuskatConvo>();

       // ruskatConvo.talkable += DisableSprite;

        transform.position = goal.position;
        playerRef = goal.transform.parent.gameObject;

        PlayerController playerController = playerRef.GetComponent<PlayerController>();
       

        playerController.OnRespawn += EnableSprite;
        playerController.InBattleStance += DisableSprite;
        playerController.NotInBattleStance += EnableSprite;

        playerController.Painting += DisableSprite;
        playerController.NotPainting += EnableSprite;
    }

    public void DisableSprite()
    {
        StopAllCoroutines();
        unTalkable?.Invoke();
        if(visualEffect != null)
        {
            visualEffect.visualEffectAsset = returnVFX;
            visualEffect.Play();
        }
       

        if (sprite)
        {
            StartCoroutine(FadeOutSprite());
        }
    }

    Coroutine FadeIn;
    public void EnableSprite()
    {
        StopAllCoroutines();
        Talkable?.Invoke();

        if(visualEffect != null)
        {
            visualEffect.visualEffectAsset = deployVFX;
            visualEffect.Play();
        }
       

        if (FadeIn != null)
        {
            StopCoroutine(FadeIn);
        }
        FadeIn = StartCoroutine(FadeInSprite());
    }

    IEnumerator FadeOutSprite()
    {
        yield return new WaitForSeconds(0.2f);
        sprite.enabled = false;
    }

    IEnumerator FadeInSprite()
    {
        yield return new WaitForSeconds(0.8f);
        sprite.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
        Vector3 direction = lookAtGoal - transform.position;

        transform.rotation = goal.rotation;

        float yAxis = WaveAmplitude * Mathf.Sin(Time.time * 2) + transform.position.y;
        transform.position = new Vector3(transform.position.x, yAxis, transform.position.z);

        if (Vector3.Distance(lookAtGoal, transform.position) > 0)
        {
            sprite.flipX = goal.transform.localPosition.x < 1; 
            transform.position = Vector3.Lerp(transform.position, goal.position, Time.deltaTime * RuskatSpeed);
           // transform.position = Vector3.MoveTowards(transform.position, goal.position, Time.deltaTime * RuskatSpeed);
        }
    }
}
