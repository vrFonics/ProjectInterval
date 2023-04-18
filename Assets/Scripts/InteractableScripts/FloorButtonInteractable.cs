using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FloorButtonInteractable : Interactable
{
    private int _offAnimIndex;
    private int _onAnimIndex;

    public float checkRadius;

    private bool _playerInRange = false;

    private bool _alreadyPressed = false;

    private bool _alreadyAnimated = true;

    public StudioEventEmitter soundEmitter;

    public SpriteRenderer spriteRenderer;

    public Sprite offSprite;
    public Sprite onSprite;

    public AnimationController animController;

    private VirtualInputManager _vim;

    public override void Start()
    {
        base.Start();
        SetState(defaultState);
        _vim = VirtualInputManager.Instance;
        _offAnimIndex = animController.GetIndexOfAnimation("off");
        _onAnimIndex = animController.GetIndexOfAnimation("on");
        if (defaultState == true)
        {
            spriteRenderer.sprite = onSprite;
        }
        else
        {
            spriteRenderer.sprite = offSprite;
        }
    }

    private void Update()
    {
        AnimateButton();
    }

    private void FixedUpdate()
    {
        _playerInRange = CheckForPlayer();
        if (_vim.interactPressed)
        {
            if (_playerInRange && !_alreadyPressed)
            {
                if (GetState() == false)
                {
                    SetState(true);
                }
                else
                {
                    SetState(false);
                }
                _alreadyPressed = true;
                _alreadyAnimated = false;
                soundEmitter.Play();
            }
        }
        else
        {
            _alreadyPressed = false;
        }
    }

    private void AnimateButton()
    {
        if (GetState() == true && _alreadyAnimated == false)
        {
            animController.PlayAnimation(_onAnimIndex, 0);
            _alreadyAnimated = true;
        }
        else if (GetState() == false && _alreadyAnimated == false)
        {
            animController.PlayAnimation(_offAnimIndex, 0);
            _alreadyAnimated = true;
        }
    }

    private bool CheckForPlayer()
    {
        return Physics2D.OverlapCircle(transform.position, checkRadius, LayerMask.GetMask("Player"));
    }

}
