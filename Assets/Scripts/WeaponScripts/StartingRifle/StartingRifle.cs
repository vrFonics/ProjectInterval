using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class StartingRifle : HitscanWeapon
{
    public Animator animator;
    public AnimationController animController;

    public ParticleSystem muzzleFlash;
    public GameObject pointLight;

    private int _fireAnimationIndex;
    private int _reloadAnimationIndex;
    private int _idleAnimationIndex;

    [SerializeField]
    private StudioEventEmitter fireEmitter;
    [SerializeField]
    private StudioEventEmitter reloadEmitter;
    [SerializeField]
    private StudioEventEmitter equipEmitter;

    public override void Start()
    {
        _fireAnimationIndex = animController.GetIndexOfAnimation("startingRifleFire");
        _reloadAnimationIndex = animController.GetIndexOfAnimation("startingRifleReload");
        _idleAnimationIndex = animController.GetIndexOfAnimation("startingRifleIdle");

        base.Start();
        pointLight.SetActive(false);
        equipEmitter.Play();
    }
    public override void Update()
    {
        base.Update();
    }

    private void OnEnable()
    {
        if (!equipEmitter.IsPlaying())
        {
            equipEmitter.Play();
        }
    }

    public override void DealDamage()
    {
        base.DealDamage();
    }

    override public void DoFireActions()
    {
        animController.PlayAnimation(_fireAnimationIndex, 0);
        muzzleFlash.Play();
        pointLight.SetActive(true);
        Invoke("SetLightInactive", 0.1f);
        fireEmitter.Play();
        Invoke("SetIdle", fireTime);
    }

    public override void DoReloadActions()
    {
        base.DoReloadActions();
        CancelInvoke("SetIdle");
        animController.PlayAnimation(_reloadAnimationIndex, 0);
        reloadEmitter.Play();
        Invoke("SetIdle", reloadTime);
    }

    private void SetLightInactive()
    {
        pointLight.SetActive(false);
    }

    private void SetIdle()
    {
        animController.PlayAnimation(_idleAnimationIndex, 0);
    }
}
