using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public BaseEntity hitEntity;
    public float velocityThreshold = 100f;
    public Rigidbody2D rb;
    public float factor = 0.2f;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitEntity = collision.collider.GetComponent<BaseEntity>();
        if (hitEntity != null && (collision.relativeVelocity.magnitude * rb.mass) / factor > velocityThreshold)
        {
            //Debug.Log("player killed");
            //Debug.Log(collision.relativeVelocity.magnitude);
            hitEntity.RemoveHealth((int)collision.relativeVelocity.magnitude);
        }
    }
}
