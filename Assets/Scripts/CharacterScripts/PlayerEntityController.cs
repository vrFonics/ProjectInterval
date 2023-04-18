using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntityController : CreatureEntity
{
    protected int WalkAnimationIndex;
    protected int IdleAnimationIndex;

    private bool _jumpAlreadyPressed;

    private VirtualInputManager _vim;
    private DeviceInput _deviceInput;

    public AnimationController animationController;

    public GameObject aimReticle;

    protected override void Start()
    {
        base.Start();
    }

    protected override void InitializeVariables()
    {
        base.InitializeVariables();
        _jumpAlreadyPressed = false;
        IdleAnimationIndex = animationController.GetIndexOfAnimation("playerIdle");
        WalkAnimationIndex = animationController.GetIndexOfAnimation("playerWalk");
        _vim = VirtualInputManager.Instance;
        _deviceInput = FindObjectOfType<DeviceInput>();
    }

    protected void FixedUpdate()
    {
        CheckForInput();
    }

    protected override void Update()
    {
        base.Update();
        AnimatePlayer();
        if (!IsAlive && _deviceInput.playerInputEnabled)
        {
            _deviceInput.playerInputEnabled = false;
        }
        else if (IsAlive && !_deviceInput.playerInputEnabled)
        {
            _deviceInput.playerInputEnabled = true;
        }
    }

    private void CheckForInput()
    {
        if (_vim.jumpPressed && !_jumpAlreadyPressed)
        {
            Jump();
            _jumpAlreadyPressed = true;
        }
        if (!_vim.jumpPressed)
        {
            _jumpAlreadyPressed = false;
        }
        if (_vim.movementVector.x != 0)
        {
            if (_vim.sprintPressed)
            {
                Walk(sprintSpeed * _vim.movementVector.x);
            }
            else
            {
                Walk(moveSpeed * _vim.movementVector.x);
            }
        }
        else
        {
            if (rigidBody.velocity.x != 0)
            {
                MoveEntity(new Vector2(-rigidBody.velocity.x, 0));
            }
        }
    }

    private void AnimatePlayer()
    {
        if (transform.position.x < aimReticle.transform.position.x)
        {
            FacingForward = true;
        }
        else
        {
            FacingForward = false;
        }
        if (_vim.movementVector.x < -0.01 || _vim.movementVector.x > 0.01)
        {
            animationController.PlayAnimation(WalkAnimationIndex, 0);
        }
        else
        {
            animationController.PlayAnimation(IdleAnimationIndex, 0);
        }
    }
}
