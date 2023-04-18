using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureEntity : RigidbodyEntity
{
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected float sprintSpeed;
    [SerializeField]
    protected float jumpForce;
    [SerializeField]
    protected float maxVelocity;
    [SerializeField]
    protected float checkRadius;
    [SerializeField]
    protected float rayDist;
    [SerializeField]
    protected float maxYVelocity = 0f;
    [SerializeField]
    protected float fallDamageThreshold = -10f;
    [SerializeField]
    protected float fallDamageMultiplier = 2f;

    protected bool IsGrounded;
    
    [SerializeField]
    protected Transform feetPos;
    [SerializeField]
    protected LayerMask groundLayer;

    protected override void Update()
    {
        base.Update();
        CheckIfGrounded();
    }

    public void CheckIfGrounded()
    {
        IsGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);
        if (!IsGrounded)
        {
            if (this.rigidBody.velocity.y < maxYVelocity)
            {
                maxYVelocity = this.rigidBody.velocity.y;
            }
        }
        if (maxYVelocity <= fallDamageThreshold && IsGrounded)
        {
            this.RemoveHealth((int)(maxYVelocity * -1 * fallDamageMultiplier));
            maxYVelocity = 0f;
        }
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            MoveEntity(new Vector2(0f, (jumpForce + (0 - rigidBody.velocity.y))));
        }
    }

    public void Walk(float speed)
    {
        Vector2 targetVelocity = Vector2.right * speed;
        Vector2 velocityChange = targetVelocity - rigidBody.velocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocity, maxVelocity);
        velocityChange.y = 0;
        MoveEntity(velocityChange);
    }
}
