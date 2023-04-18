using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class PressurePlateInteractable : Interactable
{
    [SerializeField]
    protected AnimationController animationController;
    [SerializeField]
    protected Animator animator;

    [SerializeField]
    protected StudioEventEmitter closeEmitter;
    [SerializeField]
    protected StudioEventEmitter openEmitter;

    public float massThreshold;
    private int _collidersInTrigger = 0;

    private int _closeAnimation;
    private int _openAnimation;

    public override void Start()
    {
        base.Start();
        _closeAnimation = animationController.GetIndexOfAnimation("pressurePlateClose");
        _openAnimation = animationController.GetIndexOfAnimation("pressurePlateOpen");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>().mass > massThreshold)
        {
            if (GetState() == false)
            {
                SetState(true);
                animationController.PlayAnimation(_closeAnimation, 0);
                closeEmitter.Play();
            }
            _collidersInTrigger++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>().mass > massThreshold)
        {
            _collidersInTrigger--;
        }
        if (_collidersInTrigger == 0)
        {
            if (GetState() == true)
            {
                SetState(false);
                animationController.PlayAnimation(_openAnimation, 0);
                openEmitter.Play();
            }
        }
    }
}
