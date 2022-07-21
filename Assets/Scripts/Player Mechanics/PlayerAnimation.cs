using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AnimationState
{
    Idle,
    Walk,
    Run,
    Back_Walk,
    Back_Run,
    Attack,
    Block,
    Hurt,
    Wave_Paint_Front,
    Wave_Paint_Back,
    Sad
};
public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    public AnimationState currentState;
    //bool isHolding;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeAnimationState(AnimationState newState)
    {
       // Debug.Log("Change Movement");
        if (currentState == newState) return;

 //       anim.Play(newState);

        PlayState(newState);

        currentState = newState;
    }

    public void PlayState(AnimationState state)
    {
        
        switch (state)
        {
            case AnimationState.Idle:
                anim.Play("Player_Idle");
                break;
            case AnimationState.Run:
                anim.Play("Player_Run");
                break;
            case AnimationState.Walk:
                anim.Play("Player_Walk");
                break;

            case AnimationState.Back_Run:
                anim.Play("Player_Run_Back");
                break;

            case AnimationState.Back_Walk:
                anim.Play("Player_Walk_Back");
                break;
            case AnimationState.Attack:
                anim.Play("Player_Attack");
                break;
            case AnimationState.Block:
                anim.Play("Player_Block");
                break;
            case AnimationState.Hurt:
                anim.Play("Player_Hurt");
                break;
            case AnimationState.Wave_Paint_Front:
                anim.Play("Player_Wave_Paint_F");
                break;

            case AnimationState.Wave_Paint_Back:
                anim.Play("Player_Wave_Paint_B");
                break;

            case AnimationState.Sad:
                anim.Play("Player_Sad");
                break; 
        }
    }

    public bool KeyChecker(KeyCode keyCode)
    {
        bool isHolding = false;
        if (Input.GetKeyDown(keyCode))
        {
            isHolding = true;
        }

        if (Input.GetKeyUp(keyCode))
        {
            isHolding = false;
        }

        return isHolding;
        
    }

}
