using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class HitscanWeapon : Weapon
{
    private int _layerMask;
    public float bulletTrailFadeTime;
    public LineRenderer bulletTrailLineRenderer;

    public override void Start()
    {
        base.Start();
        _layerMask = ~(LayerMask.GetMask("Player"));
    }

    public override void Fire()
    {
        base.Fire();
        if (currentMagAmmo > 0 && (timeToFire == 0))
        {
            DealDamage();
        }
    }

    public override void DoFireActions()
    {
        base.DoFireActions();
    }

    public virtual void DealDamage()
    {
        Vector2 rightVector = transform.right;
        RaycastHit2D newRay = Physics2D.Raycast(transform.position, rightVector, Mathf.Infinity, _layerMask);
        //TODO this is expensive, refactor as interface 
        if (newRay.collider == null)
        {
            DrawBulletTrail(transform.position + new Vector3(rightVector.x, rightVector.y, transform.position.z));
            return;
        }

        BaseEntity hitEntity = newRay.collider.GetComponent<BaseEntity>();
        if (hitEntity != null)
        {
            hitEntity.RemoveHealth(damage);
        }

        Rigidbody2D hitRigidBody = newRay.collider.GetComponent<Rigidbody2D>();
        if (hitRigidBody != null)
        {
            hitRigidBody.AddForce((newRay.collider.transform.position - transform.position).normalized * knockbackForce,
                ForceMode2D.Impulse);
        }

        DrawBulletTrail(newRay.point);
    }

    public virtual void DrawBulletTrail(Vector3 endPosition)
    {
        LineRenderer lineRenderer = Instantiate(bulletTrailLineRenderer);
        lineRenderer.positionCount = 2;
        Vector3[] linePositions = new Vector3[2];
        linePositions[0] = transform.position;
        linePositions[1] = endPosition;
        lineRenderer.SetPositions(linePositions);
        StartCoroutine(DestroyLineRenderer(bulletTrailFadeTime, lineRenderer));
    }

    public virtual IEnumerator DestroyLineRenderer(float seconds, LineRenderer lineRendererToDestroy)
    {
        yield return new WaitForSeconds(seconds);
        //Debug.Log("destroyed");
        Destroy(lineRendererToDestroy.gameObject);
    }
}
