using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkBox : MonoBehaviour
{
    public string channel;

    public int type;

    public bool state;

    public bool stateSent = false;

    public SpriteRenderer spriteRenderer;

    public InteractableChannelManager icm;

    public Interactable interactable;

    public Sprite redSprite;
    public Sprite greenSprite;
    public Sprite blueSprite;
    public Sprite purpleSprite;
    public Sprite orangeSprite;
    public Sprite yellowSprite;
    public Sprite whiteSprite;
    public Sprite blackSprite;

    private void Start()
    {
        icm = InteractableChannelManager.Instance;
        UpdateIndicatorSprite();
    }

    void Update()
    {
        if (type == 0 && interactable != null)
        {
            state = interactable.GetState();
            UpdateChannelSender();
        }
        else if (type == 1)
        {
            UpdateChannelReciever();
        }
    }

    public void UpdateChannelSender()
    {
        if (state == true && !stateSent)
        {
            icm.SetChannelByName(channel, 1);
            stateSent = true;
        }
        else if (state == false && stateSent)
        {
            icm.SetChannelByName(channel, -1);
            stateSent = false;
        }
    }

    public void ChangeChannel(string newChannel)
    {
        if (state == true && stateSent)
        {
            icm.SetChannelByName(channel, -1);
            stateSent = false;
        }
        channel = newChannel;
        UpdateIndicatorSprite();
    }

    public void UpdateChannelReciever()
    {
        state = icm.SetChannelByName(channel, 0) > 0 ? true : false;
    }

    public void UpdateIndicatorSprite()
    {
        switch (channel)
        {
            case "red":
                spriteRenderer.sprite = redSprite;
                break;
            case "green":
                spriteRenderer.sprite = greenSprite;
                break;
            case "blue":
                spriteRenderer.sprite = blueSprite;
                break;
            case "purple":
                spriteRenderer.sprite = purpleSprite;
                break;
            case "orange":
                spriteRenderer.sprite = orangeSprite;
                break;
            case "yellow":
                spriteRenderer.sprite = yellowSprite;
                break;
            case "white":
                spriteRenderer.sprite = whiteSprite;
                break;
            case "black":
                spriteRenderer.sprite = blackSprite;
                break;
            default:
                Debug.Log("No valid channel set!");
                break;
        }
    }
}
