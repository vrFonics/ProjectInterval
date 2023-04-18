using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public PlayerEntityController playerController;
    public TMPro.TextMeshProUGUI healthtext;

    void Update()
    {
        healthtext.text = playerController.GetHealth().ToString();
    }
}
