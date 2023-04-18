using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAimReticle : MonoBehaviour
{
    private SwitchInteractMode _switchInteractMode;
    public SpriteRenderer curReticleSprite;
    public Sprite aimReticle;
    public Sprite grabReticle;
    public Sprite timeloopReticle;

    private void Start()
    {
        _switchInteractMode = FindObjectOfType<SwitchInteractMode>();
    }
    void Update()
    {
        switch (_switchInteractMode.interactState)
        {
            case 0:
                curReticleSprite.sprite = aimReticle;
                break;
            case 1:
                curReticleSprite.sprite = grabReticle;
                break;
            case 2:
                curReticleSprite.sprite = timeloopReticle;
                break;
        }
    }
}
