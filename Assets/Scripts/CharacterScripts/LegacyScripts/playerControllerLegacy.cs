using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllerLegacy : MonoBehaviour
{
    public float checkRadius = 0.1f;
    public float moveSpeed = 10f;
    public float sprintSpeed = 20f;
    public float jumpForce = 5f;
    public float rayDist;
    public float maxVelocity = 10f;

    private int walkAnimationIndex;
    private int idleAnimationIndex;
    
    public SpriteRenderer playerSpriteRenderer;
    public Camera mainCamera;

    public GameObject aimReticle;

    public AnimationController animationController;

    private Rigidbody2D rb;
    public Transform feetPos;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool facingForward;
    private bool jumpAlreadyPressed = false;

    private void Start()
    {
        idleAnimationIndex = animationController.GetIndexOfAnimation("playerIdle");
        walkAnimationIndex = animationController.GetIndexOfAnimation("playerWalk");
        InitializeVariables();
        mainCamera = FindObjectOfType<Camera>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void Update()
    {
        AnimatePlayer();
    }

    private void InitializeVariables()
    {
        rb = GetComponent<Rigidbody2D>();
        facingForward = true;
    }
    
    private void MovePlayer()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);
        if (isGrounded && VirtualInputManager.Instance.jumpPressed)
        {
            if (!jumpAlreadyPressed)
            {
                rb.AddForce(new Vector2(0f, (jumpForce + (0 - rb.velocity.y))), ForceMode2D.Impulse);
                jumpAlreadyPressed = true;
            }
        }
        if (VirtualInputManager.Instance.jumpPressed != true)
        {
            jumpAlreadyPressed = false;
        }
        if (VirtualInputManager.Instance.movementVector.x != 0)
        {
            if (!VirtualInputManager.Instance.sprintPressed)
            {
                Vector2 targetVelocity = Vector2.right * VirtualInputManager.Instance.movementVector.x * sprintSpeed;
                Vector2 velocityChange = targetVelocity - rb.velocity;
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocity, maxVelocity);
                velocityChange.y = 0;
                rb.AddForce(velocityChange, ForceMode2D.Impulse);
            }
            else
            {
                Vector2 targetVelocity = Vector2.right * VirtualInputManager.Instance.movementVector.x * moveSpeed;
                Vector2 velocityChange = targetVelocity - rb.velocity;
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocity, maxVelocity);
                velocityChange.y = 0;
                rb.AddForce(velocityChange, ForceMode2D.Impulse);
            }
        }
        else
        {
            if (rb.velocity.x != 0)
            {
                rb.AddForce(new Vector2(-rb.velocity.x, 0), ForceMode2D.Impulse);
            }
        }
    }

    private void AnimatePlayer()
    {
        if (transform.position.x < aimReticle.transform.position.x)
        {
            facingForward = true;
        }
        else
        {
            facingForward = false;
        }
        if (VirtualInputManager.Instance.movementVector.x < -0.01)
        {
            if (facingForward)
            {
                animationController.PlayAnimation(walkAnimationIndex, 0);
            }
            else
            {
                animationController.PlayAnimation(walkAnimationIndex, 0);
            }
        }
        else if (VirtualInputManager.Instance.movementVector.x > 0.01)
        {
            if (!facingForward)
            {
                animationController.PlayAnimation(walkAnimationIndex, 0);
            }
            else
            {
                animationController.PlayAnimation(walkAnimationIndex, 0);
            }
        }
        else if (VirtualInputManager.Instance.movementVector.x == 0)
        {
            animationController.PlayAnimation(idleAnimationIndex, 0);
        }
        switch (facingForward)
        {
            case true:
                playerSpriteRenderer.flipX = false;
                break;
            case false:
                playerSpriteRenderer.flipX = true;
                break;
        }
    }
}
