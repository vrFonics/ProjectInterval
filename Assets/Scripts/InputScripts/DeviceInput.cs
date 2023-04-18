using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceInput : MonoBehaviour
{
    public PlayerInput playerInput;
    private VirtualInputManager _vim;

    public InputAction movementVector;
    public InputAction reticleVector;
    public Vector2 reticleHorizontal;
    public InputAction sprintPressed;
    public InputAction jumpPressed;
    public InputAction timeloopPressed;
    public InputAction grabPressed;
    public InputAction weaponPressed;
    public InputAction firePressed;
    public InputAction reloadPressed;
    public InputAction interactPressed;
    public bool playerInputEnabled = true;

    private void Start()
    {
        _vim = VirtualInputManager.Instance;
        playerInput = FindObjectOfType<PlayerInput>();
        movementVector = playerInput.actions.FindAction("Move");
        //Controller right thumbstick
        reticleVector = playerInput.actions.FindAction("Reticle");
        sprintPressed = playerInput.actions.FindAction("Sprint");
        jumpPressed = playerInput.actions.FindAction("Jump");
        timeloopPressed = playerInput.actions.FindAction("Timeloop Mode");
        grabPressed = playerInput.actions.FindAction("Equip Grabray");
        weaponPressed = playerInput.actions.FindAction("Equip Weapon");
        firePressed = playerInput.actions.FindAction("Fire");
        reloadPressed = playerInput.actions.FindAction("Reload");
        interactPressed = playerInput.actions.FindAction("Interact");
    }

    void Update()
    {
        if (playerInputEnabled)
        {
            CheckInputs();
        }
    }

    private void CheckInputs()
    {
        _vim.movementVector = movementVector.ReadValue<Vector2>();
        _vim.reticleVector = reticleVector.ReadValue<Vector2>();
        _vim.sprintPressed = (sprintPressed.ReadValue<float>() > 0) ? true : false;
        _vim.jumpPressed = (jumpPressed.ReadValue<float>() > 0) ? true : false;
        _vim.timeloopPressed = (timeloopPressed.ReadValue<float>() > 0) ? true : false;
        _vim.grabPressed = (grabPressed.ReadValue<float>() > 0) ? true : false;
        _vim.weaponPressed = (weaponPressed.ReadValue<float>() > 0) ? true : false;
        _vim.firePressed = (firePressed.ReadValue<float>() > 0) ? true : false;
        _vim.reloadPressed = (reloadPressed.ReadValue<float>() > 0) ? true : false;
        _vim.interactPressed = (interactPressed.ReadValue<float>() > 0) ? true : false;
    }
}
