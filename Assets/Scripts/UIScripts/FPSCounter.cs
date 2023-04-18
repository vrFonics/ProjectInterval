using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FPSCounter : MonoBehaviour
{
    public Text text;
    public Text lowestFPStext;
    private void Awake()
    {
        Debug.unityLogger.logEnabled = true;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        GetFPS();
    }
    public void GetFPS()
    {
        int current = (int)(1f / Time.unscaledDeltaTime);
        text.text = current.ToString();
    }
}
