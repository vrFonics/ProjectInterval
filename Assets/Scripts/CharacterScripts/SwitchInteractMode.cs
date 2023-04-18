using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental;

public class SwitchInteractMode : MonoBehaviour
{
    public int interactState;
    private int _previousInteractState;
    public float slowDownFactor = 0.05f;
    private float _normalFdt;

    public VirtualInputManager vim;

    public SpriteRenderer gun;
    public SpriteRenderer rayGrab;

    public GameObject currentWeapon;
    public GameObject rayGrabber;

    private UnityEngine.Rendering.Volume _timeloopVolume;

    public void Start()
    {
        foreach (UnityEngine.Rendering.Volume volumeToCheck in FindObjectsOfType<UnityEngine.Rendering.Volume>())
        {
            if (volumeToCheck.gameObject.name == "TimeloopVolume")
            {
                _timeloopVolume = volumeToCheck;
            }
        }
        _normalFdt = Time.fixedDeltaTime;
        interactState = 0;
        currentWeapon.SetActive(true);
        rayGrabber.SetActive(false);
        vim = VirtualInputManager.Instance;
    }
    public void DoSwitchInteractMode()
    {
        bool timeloopHeld = vim.timeloopPressed;
        bool grabPressed = vim.grabPressed;
        bool weaponPressed = vim.weaponPressed;
        if (timeloopHeld)
        {
            interactState = 2;
        }
        else
        {
            if (grabPressed && !rayGrabber.activeSelf)
            {
                interactState = 1;
                _previousInteractState = 1;
                currentWeapon.SetActive(false);
                rayGrabber.SetActive(true);
            }
            else if (weaponPressed && !currentWeapon.activeSelf)
            {
                interactState = 0;
                _previousInteractState = 0;
                currentWeapon.SetActive(true);
                rayGrabber.SetActive(false);
            }
            else
            {
                interactState = _previousInteractState;
            }
        }

    }
    void Update()
    {
        DoSwitchInteractMode();
        DoTimeloopEffects();
    }

    void DoTimeloopEffects()
    {
        if (interactState == 2)
        {
            Time.timeScale = slowDownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            //timeloopVolume.enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = _normalFdt;
            //timeloopVolume.enabled = false;
        }
    }
}
