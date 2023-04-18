using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevelDoor : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string doorCloseEvent;

    public bool opened = false;
    public bool soundPlaying = false;

    private readonly float _frameTime = 0.125f;
    private float _timeToNext = 0;

    public int timesToPlay = 22;

    private int _timesPlayed = 0;
    private int _oppositeFrame;
    private int _openAnimationIndex;
    private int _closeAnimationIndex;
    private int _idleAnimationIndex;
    private int _frameCount;

    private FMOD.Studio.EventInstance _newInstance;

    public Sprite onIndicatorSprite;
    public Sprite offIndicatorSprite;

    public GameObject doorObject;

    public LinkBox doorLinkBox;

    public BoxCollider2D boxCollider;

    public Rigidbody2D rb;

    [SerializeField]
    public AnimationController animController;

    private void Start()
    {
        _openAnimationIndex = animController.GetIndexOfAnimation("exitLevelDoorOpen");
        _closeAnimationIndex = animController.GetIndexOfAnimation("exitLevelDoorClose");
        _idleAnimationIndex = animController.GetIndexOfAnimation("exitLevelDoorIdle");
        _frameCount = animController.GetFrameCount(_openAnimationIndex);
        timesToPlay = _frameCount;
    }

    void FixedUpdate()
    {
        if (doorLinkBox.state == true && !opened)
        {
            if (_timesPlayed != 0)
            {
                _oppositeFrame = animController.currentAnimation.frames.Count - (animController.GetCurrentFrame() - 1);
                animController.PlayAnimation(_openAnimationIndex, _oppositeFrame);
                timesToPlay = _frameCount - _oppositeFrame;
            }
            else
            {
                animController.PlayAnimation(_openAnimationIndex, 0);
                timesToPlay = _frameCount;
            }
            opened = true;
            _timeToNext = _frameTime;
            _timesPlayed = 0;
            soundPlaying = true;
        }
        if (doorLinkBox.state == false && opened == true)
        {
            if (_timesPlayed != 0)
            {
                _oppositeFrame = animController.currentAnimation.frames.Count - (animController.GetCurrentFrame() - 1);
                animController.PlayAnimation(_closeAnimationIndex, _oppositeFrame);
                timesToPlay = _frameCount - _oppositeFrame;
            }
            else
            {
                animController.PlayAnimation(_closeAnimationIndex, 0);
                timesToPlay = _frameCount;
            }
            opened = false;
            _timeToNext = _frameTime;
            _timesPlayed = 0;
            soundPlaying = true;
        }
        if (_timesPlayed < timesToPlay + 1 && soundPlaying)
        {
            if (_timeToNext >= _frameTime)
            {
                _timeToNext = 0;
                _newInstance = new FMOD.Studio.EventInstance();
                _newInstance = FMODUnity.RuntimeManager.CreateInstance(doorCloseEvent);
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(_newInstance, transform, rb);
                _newInstance.start();
                if (opened && animController.GetCurrentFrame() != 0) {
                    
                    boxCollider.offset -= new Vector2(0, 2 / (float)_frameCount);
                }
                if (!opened && animController.GetCurrentFrame() != 0 && animController.GetCurrentFrame() != _frameCount)
                {
                    boxCollider.offset += new Vector2(0, 2 / (float)_frameCount);
                }
                _timesPlayed++;
                //Debug.Log("played");
            }
        }
        else
        {
            _timesPlayed = 0;
            soundPlaying = false;
            if (!opened)
            {
                animController.PlayAnimation(_idleAnimationIndex, 0);
            }
            return;
        }
        _timeToNext += Time.fixedDeltaTime;
    }
}
