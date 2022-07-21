using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator anim;
    //AnimationClip[] animationClips;
    public List<AnimationClip> animationClips;


    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        GetAllAnimations();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetToIdle()
    {
        anim.Play(animationClips[0].name);
    }

    public void GetAllAnimations()
    {
        foreach (AnimationClip ac in anim.runtimeAnimatorController.animationClips)
        {
            animationClips.Add(ac);
        }
    }
}