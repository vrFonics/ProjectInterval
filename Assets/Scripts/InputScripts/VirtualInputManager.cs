using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualInputManager : MonoBehaviour
{
    public static VirtualInputManager Instance = null;

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

    public bool jumpPressed;
    public bool sprintPressed;
    public bool grabPressed;
    public bool weaponPressed;
    public bool interactPressed;
    public bool timeloopPressed;
    public bool firePressed;
    public bool reloadPressed;

    public Vector2 movementVector;

    public Vector2 reticleVector;

}
