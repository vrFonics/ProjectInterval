using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private List<AnimationContainer> animations;
    public AnimationContainer currentAnimation;

    private bool playing;

    private int currentFrame;

    private float timeToNextFrame;

    public void Start()
    {
        currentAnimation = null;
    }

    public void Update()
    {
        if (playing)
        {
            if (currentFrame < currentAnimation.frames.Count)
            {
                Animate();
            }
            else
            {
                currentAnimation = null;
                playing = false;
            }
        }
    }

    private void Animate()
    {
        if (timeToNextFrame <= 0)
        {
            spriteRenderer.sprite = currentAnimation.frames[currentFrame];
            timeToNextFrame = 1 / (float)currentAnimation.framerate;
            currentFrame++;
        }
        timeToNextFrame -= Time.deltaTime;
    }

    public void PlayAnimation(int index, int frameToStart)
    {
        if (currentAnimation == null) {
            SetAnimation(index, frameToStart);
        }
        else if (currentAnimation.name != animations[index].name)
        {
            SetAnimation(index, frameToStart);
        }
    }

    private void SetAnimation(int index, int frameToStart)
    {
        currentAnimation = animations[index];
        playing = true;
        currentFrame = frameToStart;
        timeToNextFrame = 0;
    }

    public int GetIndexOfAnimation(string animationName)
    {
        foreach (AnimationContainer animation in animations)
        {
            if (animation.name == animationName)
            {
                return animations.IndexOf(animation);
            }
        }
        Debug.Log("Animation not found");
        return 0;
    }

    public int GetFrameCount(int index)
    {
        return animations[index].frames.Count;
    }

    public int GetCurrentFrame()
    {
        return currentFrame;
    }
}
