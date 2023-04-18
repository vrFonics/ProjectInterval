using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;

public class ANDGate : LogicGate
{
    private bool outputState = false;

    void Update()
    {
        DoLogic();
        UpdateSprite();
    }

    void DoLogic()
    {
        if ((inputs[0].powered == true) && (inputs[1].powered == true) && (outputState == false))
        {
            outputs[0].UpdateNetworkPower(true);
            outputState = true;
        }
        else
        {
            if ((inputs[0].powered == true && (inputs[1].powered == true)))
            {
                return;
            }
            if (outputState == true)
            {
                outputs[0].UpdateNetworkPower(false);
                outputState = false;
            }
        }
    }

    void UpdateSprite()
    {
        if (inputs[0].powered == true && inputs[1].powered == true)
        {
            spriteRenderer.sprite = sprites[3];
        }
        else if (inputs[0].powered == true && inputs[1].powered == false)
        {
            spriteRenderer.sprite = sprites[1];
        }
        else if (inputs[0].powered == false && inputs[1].powered == true)
        {
            spriteRenderer.sprite = sprites[2];
        }
        else
        {
            spriteRenderer.sprite = sprites[0];
        }
    }
}
