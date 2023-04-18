using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : CreatureEntity
{
    Transform target;

    public bool hasTarget = false;

    public GameObject player;

    public float sightDistance;
    public float buffer;

    public LayerMask enemyLayerMask;

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<PlayerEntityController>().gameObject;
        enemyLayerMask = ~(LayerMask.GetMask("Enemy"));
    }

    protected override void Update()
    {
        base.Update();
        if (!hasTarget)
        {
            FindTarget();
        }
        else
        {
            MoveToTarget();
        }
    }

    protected virtual void MoveToTarget()
    {
        if (Mathf.Abs((transform.position.x - target.position.x)) < buffer)
        {
            return;
        }
        if (target.position.x < transform.position.x)
        {
            Walk(-moveSpeed);
        }
        else
        {
            Walk(moveSpeed);
        }
    }

    protected virtual void FindTarget()
    {
        if (CheckLineOfSight(sightDistance, player))
        {
            target = player.transform;
            hasTarget = true;
        }
    }

    protected virtual bool CheckLineOfSight(float distance, GameObject target)
    {
        RaycastHit2D newRay = Physics2D.Raycast(transform.position, target.transform.position - transform.position, distance, enemyLayerMask);
        Debug.Log(newRay.collider.gameObject);
        if (newRay.collider.gameObject == target.gameObject)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
