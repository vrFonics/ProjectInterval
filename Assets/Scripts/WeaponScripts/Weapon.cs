using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int maxAmmo;
    public int magSize;
    public int currentMagAmmo;
    public int currentAmmo;
    public int startingAmmo;

    public float fireTime;
    public float reloadTime;
    public float damage;
    public float knockbackForce;

    public VirtualInputManager vim;
    public SwitchInteractMode switchIMode;

    public float timeToFire = 0f;

    public bool firing;
    public bool automatic;
    public bool holdingFire = false;
    public bool reloading = false;

    virtual public void Start()
    {
        currentAmmo = startingAmmo;
        if (currentAmmo <= magSize)
        {
            currentMagAmmo = currentAmmo;
            currentAmmo = 0;
        }
        else
        {
            currentMagAmmo = magSize;
            currentAmmo -= magSize;
        }
        
        vim = VirtualInputManager.Instance;
        switchIMode = FindObjectOfType<SwitchInteractMode>();
    }
    virtual public void Update()
    {
        if (firing)
        {
            timeToFire += Time.deltaTime;
        }
        if (timeToFire >= fireTime)
        {
            timeToFire = 0;
            firing = false;
        }
        if (!vim.firePressed)
        {
            holdingFire = false;
        }
        if (switchIMode.interactState != 0)
        {
            CancelInvoke("Reload");
            reloading = false;
        }
        if (switchIMode.interactState == 0 && vim.firePressed && !reloading)
        {
            if (automatic)
            {
                Fire();
            }
            else
            {
                if (!holdingFire)
                {
                    Fire();
                }
            }
        }
        if (switchIMode.interactState == 0 && vim.reloadPressed && currentMagAmmo != magSize && currentAmmo != 0 && !reloading)
        {
            DoReloadActions();
        }
    }

    virtual public void Fire()
    {
        if (currentMagAmmo > 0 && timeToFire == 0)
        {
            currentMagAmmo -= 1;
            DoFireActions();
            firing = true;
        }
        holdingFire = true;
    }
    virtual public void DoReloadActions()
    {
        reloading = true;
        Invoke("Reload", reloadTime);
    }

    virtual public void DoFireActions()
    {
    }

    private void Reload()
    {
        reloading = false;
        if (currentMagAmmo == 0)
        {
            if (currentAmmo >= magSize)
            {
                currentMagAmmo = magSize;
                currentAmmo -= magSize;
            }
            else
            {
                currentMagAmmo = currentAmmo;
                currentAmmo = 0;
            }
        }
        else
        {
            if (currentAmmo + currentMagAmmo > magSize)
            {
                currentAmmo -= magSize - currentMagAmmo;
                currentMagAmmo = magSize;
            }
            else
            {
                currentMagAmmo += currentAmmo;
                currentAmmo = 0;
            }
        }
    }
}
