using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private bool state;
    public bool defaultState;
    public virtual void Start()
    {
        SetState(defaultState);
    }
    public bool GetState()
    {
        return state;
    }

    public void SetState(bool newState)
    {
        state = newState;
    }
}
