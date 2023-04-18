using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquippedIndicator_UIElement : MonoBehaviour
{
    public UnityEngine.UI.Image indicatorUIElement;
    public TMPro.TextMeshProUGUI ammoCounterTotalText;
    public TMPro.TextMeshProUGUI ammoCounterMagText;

    public Sprite grabIndicatorSprite;
    public Sprite ammoIndicatorSprite;
    public Sprite timeloopIndicatorSprite;

    public SwitchInteractMode swm;
    public StartingRifle sr;

    private int _interactState;

    private void Start()
    {
        swm = FindObjectOfType<SwitchInteractMode>();
        sr = FindObjectOfType<StartingRifle>();
    }

    void Update()
    {
        _interactState = swm.interactState;
        ammoCounterTotalText.text = sr.currentAmmo.ToString();
        ammoCounterMagText.text = sr.currentMagAmmo.ToString();
        switch (_interactState) {
            case 0:
                indicatorUIElement.sprite = ammoIndicatorSprite;
                break;

            case 1:
                indicatorUIElement.sprite = grabIndicatorSprite;
                break;

            case 2:
                indicatorUIElement.sprite = timeloopIndicatorSprite;
                break;

            default:
                break;
        }
    }
}
