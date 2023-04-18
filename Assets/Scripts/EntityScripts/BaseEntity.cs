using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    [SerializeField]
    protected float healthCap;
    [SerializeField]
    protected float currentHealth;

    [SerializeField]
    protected int entityID;

    protected bool IsAlive;
    [SerializeField]
    protected bool isInvincible;
    protected bool FacingForward;

    protected Camera MainCamera;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        InitializeVariables();
    }

    protected virtual void InitializeVariables()
    {
        MainCamera = FindObjectOfType<Camera>();
        FacingForward = true;
        if (currentHealth > 0)
        {
            IsAlive = true;
        }
    }

    protected virtual void Update()
    {
        UpdateSpriteDirection();
    }

    public void RefreshVariables()
    {
        InitializeVariables();
    }

    public int GetEntityID()
    {
        return entityID;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public float AddHealth(float newHealth)
    {
        float oldHealth = currentHealth;
        currentHealth += newHealth;
        if (currentHealth > healthCap)
        {
            currentHealth = healthCap;
        }
        float healthAdded = currentHealth - oldHealth;
        return healthAdded;
    }

    public float RemoveHealth(float newHealth)
    {
        float oldHealth = currentHealth;
        currentHealth -= newHealth;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        float healthTaken = oldHealth - currentHealth;
        return healthTaken;
    }

    public bool GetIsAlive()
    {
        return IsAlive;
    }

    public void SetIsAlive(bool newAliveState)
    {
        IsAlive = newAliveState;
        if (!IsAlive)
        {
            Die();
        }
    }

    public void SetIsInvincible(bool newInvincibleState)
    {
        isInvincible = newInvincibleState;
    }

    protected virtual void Die()
    {
        if (isInvincible)
        {
            return;
        }
        IsAlive = false;
    }

    private void UpdateSpriteDirection()
    {
        switch (FacingForward)
        {
            case true:
                spriteRenderer.flipX = false;
                break;
            case false:
                spriteRenderer.flipX = true;
                break;
        }
    }
}
