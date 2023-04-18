using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOTGate : LogicGate
{

    private bool outputState = false;

    void Update()
    {
        DoLogic();
        UpdateSprite();
    }

    void DoLogic()
    {
        if (inputs[0].powered == true && outputState == true)
        {
            outputs[0].UpdateNetworkPower(false);
            outputState = false;
        }
        else if (inputs[0].powered == false && outputState == false)
        {
                outputs[0].UpdateNetworkPower(true);
                outputState = true;
        }
    }

    void UpdateSprite()
    {
        if (inputs[0].powered == false)
        {
            spriteRenderer.sprite = sprites[0];
        }
        else if (inputs[0].powered == true)
        {
            spriteRenderer.sprite = sprites[1];
        }
    }
}
