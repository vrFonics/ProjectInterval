using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableChannelManager : MonoBehaviour
{
    public int redChannelActive;
    public int greenChannelActive;
    public int blueChannelActive;
    public int purpleChannelActive;
    public int orangeChannelActive;
    public int yellowChannelActive;
    public int whiteChannelActive;
    public int blackChannelActive;

    public static InteractableChannelManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public int SetChannelByName(string channelName, int value)
    {
        switch (channelName)
        {
            case "red":
                redChannelActive += value;
                return redChannelActive;
            case "green":
                greenChannelActive += value;
                return greenChannelActive;
            case "blue":
                blueChannelActive += value;
                return blueChannelActive;
            case "purple":
                purpleChannelActive += value;
                return purpleChannelActive;
            case "orange":
                orangeChannelActive += value;
                return orangeChannelActive;
            case "yellow":
                yellowChannelActive += value;
                return yellowChannelActive;
            case "white":
                whiteChannelActive += value;
                return whiteChannelActive;
            case "black":
                blackChannelActive += value;
                return blackChannelActive;
            default:
                Debug.Log("No valid channel set!");
                return 0;
        }
    }
}
