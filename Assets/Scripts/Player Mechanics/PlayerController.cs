using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;
public class PlayerController : MonoBehaviour
{
    Movement_CC movement;
    AttackComp attackComp;
    HealthComponent healthComponent;
    CharacterController characterController;
    public Transform AttackPoint;
    public Vector2 input = Vector2.zero;
    float facingDirX;
    public bool isAttacking = false;
    bool canAttack;
    public Animator anim;
    private float speedMag;
    bool knockedback;
    [SerializeField] GameObject RuskatPoint;

    public int keys;
    public int walkSfxIndex;
    public SoundEvent soundEvent;
    public AudioSource sfxSource;
    public AudioSource walkingSource;

    //States
    public bool battleStance { get; private set; }

    public Action InBattleStance;
    public Action NotInBattleStance;

    //Things that shouldnt be here
    public GameObject KeyVFX;
    public Action<Sprite> OnKeyPickup;
    public Action<int> OnKeyUsed;
    public Action OnRespawn;

    public Action Painting;
    public Action NotPainting;

    bool blocking = false;

    //Hard coded asf but whatever
    [SerializeField] float FlipKnockbackShitVariable = 1;

    [HideInInspector] public Vector3 CurrentCheckpoint;

    void Start()
    {
        movement = GetComponent<Movement_CC>();
        //playerAnimation = GetComponent<PlayerAnimation>();
        anim = GetComponent<Animator>();
        facingDirX = -1f;
        attackComp = GetComponent<AttackComp>();
        healthComponent = GetComponent<HealthComponent>();
        characterController = GetComponent<CharacterController>();
        healthComponent.OnDamage += OnDamage;
        healthComponent.OnZeroHealth += OnDeath;
        healthComponent.InvulHit += OnInvulHit;
        canAttack = true;
        battleStance = false;

        CurrentCheckpoint = transform.position;
            ExitBattleStance();
       // forwardKeyPressed = false;
    }

    Coroutine BlockKnockback = null;

    void OnInvulHit()
    {
        Vector3 velocity;
        if (healthComponent.DamagerData != null)
        {
            movement.velocity.x = -5f * healthComponent.DamagerData.GetComponent<EnemyMovementAI>().facingDirX * 
                FlipKnockbackShitVariable;
            movement.velocity = movement.velocity.x * transform.right;

            attackComp.source.PlayOneShot(soundEvent.clips[9]);

            StartCoroutine(SetBlockKnockback());
        }
        anim.SetBool("blockhitbool", true);
        anim.SetTrigger("blockhit");
    }

    IEnumerator SetBlockKnockback()
    {
        yield return new WaitForSeconds(0.05f);
        movement.velocity = Vector3.zero;
    }

    void OnDeath()
    {
        if (anim.GetBool("dead"))
            return;
        movement.canMove = false;
        movement.Stop();
        isAttacking = true;
        anim.SetBool("dead", true);
        attackComp.source.PlayOneShot(soundEvent.clips[8]);

        StartCoroutine(DeathTimer());
    }
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(3f);
        TeleportPlayer(CurrentCheckpoint);
        healthComponent.ResetHealth();
        GetComponent<Paint>().ResetPaint();
        OnRespawn?.Invoke();
        //movement.enabled = true;
        isAttacking = false;
        anim.SetBool("dead", false);
    }

    public void TeleportPlayer(Vector3 Location)
    {
      //  Debug.Log("teleport player " + Location);
        characterController.enabled = false;
        transform.position = Location;
        battleStance = false;
        characterController.enabled = true;
    }
    public void KeyPickedUp(Sprite sprite)
    {
        keys++;
        Debug.Log(keys);
        OnKeyPickup?.Invoke(sprite);
        KeyVFX.GetComponent<VFXHandler>().PlayVFX(VFXHandler.VFX.KeyPickup);
    }

    public void UseKey(int amount)
    {
        Debug.Log(keys + " " + amount);
        keys -= amount;
        OnKeyUsed?.Invoke(amount);
    }

    public void OnDamage(float dmg)
    {
        sfxSource.PlayOneShot(soundEvent.clips[4]);
        movement.canMove = false;

        if (healthComponent.DamagerData != null)
        {
            movement.velocity.x = -3f * healthComponent.DamagerData.GetComponent<EnemyMovementAI>().facingDirX *
                FlipKnockbackShitVariable;
            movement.velocity = movement.velocity.x * transform.right;
        }
        movement.velocity.y = Mathf.Sqrt(2 * -2f * movement.gravity);

        knockedback = true;
        anim.SetTrigger("hurt");
        KeyVFX.GetComponent<VFXHandler>().PlayVFX(VFXHandler.VFX.OnHit);
    }

    private void OnDisable()
    {
        anim.SetFloat("moveX", 0);
        anim.SetFloat("moveY", 0);
        anim.SetFloat("speed", 0);
    }

    void Update()
    {
        if (knockedback)
        {
            movement.CheckGrounded();

            if (isAttacking)
                isAttacking = false;
            if (movement.isGrounded)
            {
                movement.velocity.x = 0;
                knockedback = false;
                movement.canMove = true;
            }
            return;
        }
        if (isAttacking)
        {
            //fix jump attack animation cancel blocker
            return;
        }

        anim.SetBool("battleStance", battleStance);
        anim.SetBool("isGrounded", movement.isGrounded);
        if (movement.canMove)
        {
            speedMag = input.sqrMagnitude;
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            anim.SetBool("canAttack", canAttack);

            input = input.normalized;

            Vector3 moveDirX = input.x * transform.right;
            Vector3 moveDirZ = input.y * transform.forward;
            Vector3 moveInput = moveDirX + moveDirZ;

            movement.Move(moveInput);

            anim.SetFloat("moveX", input.x);
            anim.SetFloat("moveY", input.y);
            anim.SetFloat("speed", speedMag);
            
        }
        else
        {
            anim.SetFloat("moveX", 0);
            anim.SetFloat("moveY", 0);
            anim.SetFloat("speed", 0);
        }

        if (input.x > 0)
        {
            if (AttackPoint.transform.localPosition.x < 0)
            {
                AttackPoint.transform.localPosition = Vector3.Scale(AttackPoint.transform.localPosition, new Vector3(-1, 1, 1));
                RuskatPoint.transform.localPosition = Vector3.Scale(RuskatPoint.transform.localPosition, new Vector3(-1, 1, 1));
            }

            GetComponent<SpriteRenderer>().flipX = false;
            facingDirX = 1f;
        }
        else if (input.x < 0)
        {
            if (AttackPoint.transform.localPosition.x > 0)
            {
                AttackPoint.transform.localPosition = Vector3.Scale(AttackPoint.transform.localPosition, new Vector3(-1, 1, 1));
                RuskatPoint.transform.localPosition = Vector3.Scale(RuskatPoint.transform.localPosition, new Vector3(-1, 1, 1));
            }

            facingDirX = -1f;
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (!battleStance)
            return;

        if (canAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !blocking)
            {
                attackComp.source.PlayOneShot(soundEvent.clips[3]);
                //attackComp.Attack();
                anim.SetTrigger("attack");
                isAttacking = true;
                movement.Stop();
                return;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                anim.SetBool("blocking", true);
                movement.canMove = false;
                blocking = true;
                healthComponent.invulnerable = true;
                movement.Stop();
            }

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                movement.velocity.x = 0;
                blocking = false;
                movement.canMove = true;
                healthComponent.invulnerable = false;
                anim.SetBool("blocking", false);
                anim.SetBool("blockhitbool", false);
            }
        }

    }

    public void EnterBattleStance()
    {
        battleStance = true;
        InBattleStance?.Invoke();
    }

    public void ExitBattleStance()
    {
        battleStance = false;

        blocking = false;
        movement.canMove = true;
        healthComponent.invulnerable = false;
        anim.SetBool("blocking", false);
        anim.SetBool("blockhitbool", false);

        NotInBattleStance?.Invoke();
    }

    void AttackComplete()
    {
        isAttacking = false;
        movement.canMove = true;
    }

    public void footsteps()
    {
        walkingSource.Play();
    }
}
