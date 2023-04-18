using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyEntity : BaseEntity
{
    [SerializeField]
    protected float gravityScale = 1f;

    [SerializeField]
    protected bool affectedByGravity = true;

    [SerializeField]
    protected Rigidbody2D rigidBody;

    protected override void Update()
    {
        base.Update();
        if (!affectedByGravity)
        {
            rigidBody.gravityScale = 0;
        }
        else
        {
            rigidBody.gravityScale = gravityScale;
        }
    }

    public void SetAffectedByGravity(bool newGravityState)
    {
        affectedByGravity = newGravityState;
    }

    public void SetGravityScale(float newGravityScale)
    {
        gravityScale = newGravityScale;
    }

    public void MoveEntity(Vector2 newVelocity)
    {
        rigidBody.AddForce(newVelocity, ForceMode2D.Impulse);
    }
}
