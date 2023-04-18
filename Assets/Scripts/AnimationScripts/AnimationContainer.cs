using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationContainer : MonoBehaviour
{
    new public string name;
    [SerializeField]
    public List<Sprite> frames;
    public int framerate;
}
