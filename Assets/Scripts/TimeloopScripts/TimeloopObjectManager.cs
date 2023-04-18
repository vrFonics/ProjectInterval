using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeloopObjectManager : MonoBehaviour
{
    private List<GameObject> _timeloopObjects;

    public GameObject aimReticle;
    public SwitchInteractMode switchInteractMode;

    public bool fireContinuouslyHeld = false;

    private void Awake()
    {
    }

    void Start()
    {
        UpdateTimeLoopObjects();
        switchInteractMode = FindObjectOfType<SwitchInteractMode>();
        aimReticle = FindObjectOfType<AimReticle>().gameObject;
    }

    public void UpdateTimeLoopObjects()
    {
        _timeloopObjects = new List<GameObject>();
        foreach (TimeLoopObject timeloopObject in FindObjectsOfType<TimeLoopObject>())
        {
            _timeloopObjects.Add(timeloopObject.gameObject);
        }
    }

    public void CheckForTLObjectSelected()
    {
        if (VirtualInputManager.Instance.firePressed && switchInteractMode.interactState == 2 && !fireContinuouslyHeld)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(aimReticle.transform.position, 0.4f);
            foreach (Collider2D hitCollider in hitColliders)
            {
                if (_timeloopObjects.Contains(hitCollider.gameObject))
                {
                    if (hitCollider.GetComponent<TimeLoopObject>().recorded == false)
                    {
                        hitCollider.GetComponent<TimeLoopObject>().SetRecording();
                        fireContinuouslyHeld = true;
                    }
                    else
                    {
                        hitCollider.GetComponent<TimeLoopObject>().ResetRecording();
                        fireContinuouslyHeld = true;
                    }
                }
            }
        }
    }

    private void Update()
    {
        CheckForTLObjectSelected();
        if (VirtualInputManager.Instance.firePressed == false)
        {
            fireContinuouslyHeld = false;
        }
    }
}
