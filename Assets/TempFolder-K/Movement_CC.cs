using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_CC : MonoBehaviour
{
    CharacterController characterController;
    BoxCollider col;
    public LayerMask groundMask;
    PlayerController playerController;
    Animator anim;

    AttackComp attackComp;
    //Stats
    [SerializeField] float jumpForce = 30f;
    [SerializeField] float speed = 10f;
    public readonly float gravity = -9.81f * 2;

    //Info
    public Vector3 velocity;
    Vector2 input = Vector2.zero;
    public int FacingDirX;

    [Header("Variable Jump Attributes")]
    //Variable Jump
    /*
     * This makes the jump force depend on how long you hold space
     */
    [SerializeField] bool useVarJump;
    [SerializeField] float jumpTimer;
    float jumpTimeCounter = 0;
    public bool canMove = true;
    bool isJumpingVariable = false;
    bool isJumping = false;

    [Header("Ground Check")]
    [SerializeField] float m_MaxDistance;
    [SerializeField] Vector3 groundCheckSize;
    public bool isGrounded;
    RaycastHit m_Hit;
    bool startedFalling = false;

    //Jump variables
    [SerializeField] float strafeSpeedMult;
    float jumpXVelocity;
    float jumpZVelocity;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        col = GetComponent<BoxCollider>();
        FacingDirX = 1;
        attackComp = GetComponent<AttackComp>();
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    public void CheckGrounded()
    {
        anim.SetFloat("ySpeed", velocity.y);
        if (velocity.y > 0)
        {
            isGrounded = false;
            return;
        }
        isGrounded = Physics.BoxCast(col.bounds.center, groundCheckSize, -transform.up, out m_Hit, transform.rotation, m_MaxDistance, groundMask);
    }

    void OnDrawGizmos()
    {
        

        //Check if there has been a hit yet
        if (isGrounded)
        {
            Gizmos.color = Color.green;
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, -transform.up * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + -transform.up * m_Hit.distance, groundCheckSize);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            Gizmos.color = Color.red;
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, -transform.up * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + -transform.up * m_MaxDistance, groundCheckSize);
        }
    }

    void Update()
    {
        //if (isGrounded)
        //{
        //    velocity.x = 0;
        //    velocity.y = 0;
        //}
        CheckGrounded();


        if (isGrounded && velocity.y <= 0)
        {
            // Regular, on the ground, gravity.
            // Force player downwards the distance of a step as defined by the character controller. Prevents leaving the ground when going down slopes.
            velocity.y = -characterController.stepOffset / Time.deltaTime;
        //    startedFalling = false;
        }
        //else if (!isGrounded && !startedFalling)
        //{
        //    // Just left ground, not moving upwards (i.e. jumping), so we've just starting falling.
        //    // Set downwards speed to 0, otherwise we'll be forced downwards at nearly half terminal velocity.
        //    velocity.x = 0;
        //    velocity.y = 0;
        //    startedFalling = true;
        //    Debug.Log("falling");
        //}
        else if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);

        if (!canMove)
            return;
        Jump();

        //if (!isGrounded() && rb2d.velocity.y < 0)
        //{
        //    anim.SetTrigger("Falling");
        //}
    }
    public void Stop()
    {
        velocity = Vector3.zero;
        canMove = false;
    }
    public void Move(Vector3 moveInput)
    {
        if (!canMove)
            return;


        if (isGrounded)
        {
            velocity.x = moveInput.x * speed;
            velocity.z = moveInput.z * speed;
        }
        else if (moveInput != Vector3.zero)
        {
            //if the input pressed is not the same
            if (jumpXVelocity != moveInput.x * speed)
                velocity.x += moveInput.x * strafeSpeedMult;

            if (jumpZVelocity != moveInput.z * speed)
                velocity.z += moveInput.z * strafeSpeedMult;

            velocity.x = Mathf.Clamp(velocity.x, -speed, speed);
            velocity.z = Mathf.Clamp(velocity.z, -speed, speed);
        }

        Vector3 moveVel = new Vector3(velocity.x, 0, velocity.z);
        characterController.Move(moveVel * Time.deltaTime);

        if (characterController.velocity.x > 0)
            FacingDirX = 1;
        else
            FacingDirX = -1;

        //characterController.Move(moveInput * speed * Time.deltaTime);

    }
    void MovementToggle()
    {
        canMove ^= true;
        //playerController.canAttack ^= true;
        velocity = Vector3.zero;
    }

    public void EnableMove()
    {
        canMove = true;
    }
    public void Jump()
    {
        if (Input.GetKeyDown("space") && isGrounded)
        {
            //rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            velocity.y = 0;

            if (useVarJump)
            {
                jumpTimeCounter = jumpTimer;
                isJumpingVariable = true;
            }
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            jumpXVelocity = velocity.x;
            jumpZVelocity = velocity.z;

            velocity.x = jumpXVelocity;
            velocity.z = jumpZVelocity;

            if (jumpForce != 0)
            {
                anim.SetTrigger("jump");
            }
            //rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey("space") && isJumpingVariable && useVarJump)
        {
            if (jumpTimeCounter > 0)
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
                isJumpingVariable = false;
        }

        if (Input.GetKeyUp("space") && useVarJump)
        {
            isJumpingVariable = false;
        }
    }
}
