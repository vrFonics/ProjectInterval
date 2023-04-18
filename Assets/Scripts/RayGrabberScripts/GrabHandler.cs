using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHandler : MonoBehaviour
{
    private SwitchInteractMode _switchInteractMode;
    private VirtualInputManager _vim;

    private int _idleAnimIndex;
    private int _activeAnimIndex;

    public bool objectGrabbed = false;

    public float objectMoveSpeed = 2f;
    public float maxVelocity = 3f;

    public GameObject grabPathFinder;
    public GameObject grabbedObject;
    public GameObject aimReticle;
    public GameObject player;
    public Rigidbody2D objectRB;

    public AnimationController animController;

    public LineRenderer lineRenderer;

    private List<GameObject> _grabbableObjects;
    void Start()
    {
        InitializeVariables();
        UpdateGrabbableObjects();
        _switchInteractMode = FindObjectOfType<SwitchInteractMode>();
        _vim = FindObjectOfType<VirtualInputManager>();
        lineRenderer.enabled = false;
    }

    private void InitializeVariables()
    {
        _idleAnimIndex = animController.GetIndexOfAnimation("idle");
        _activeAnimIndex = animController.GetIndexOfAnimation("active");
    }
    private void FixedUpdate()
    {
        GrabObject();
        MoveGrabbedObject();
    }

    private void Update()
    {
        AnimateGrabRay();
    }

    private void AnimateGrabRay()
    {
        if (objectGrabbed)
        {
            animController.PlayAnimation(_activeAnimIndex, 0);
        }
        else
        {
            animController.PlayAnimation(_idleAnimIndex, 0);
        }
    }

    public void UpdateGrabbableObjects()
    {
        _grabbableObjects = new List<GameObject>();
        foreach (GrabbableObject grabbableObjectInScene in FindObjectsOfType<GrabbableObject>())
        {
            if (grabbableObjectInScene.enabled == true)
            {
                _grabbableObjects.Add(grabbableObjectInScene.gameObject);
            }
        }
    }
    
    void MoveGrabbedObject()
    {
        Vector3 aimReticlePosition = transform.position;
        if (objectGrabbed)
        {
            Vector3 grabbedObjectPosition = grabbedObject.transform.position;
            RaycastHit2D raycastCheck = Physics2D.Raycast(grabPathFinder.transform.position, grabbedObject.transform.position - grabPathFinder.transform.position, Mathf.Infinity, (1 << 6));
            if (raycastCheck.collider.gameObject != grabbedObject)
            {
                objectRB.gravityScale = 1;
                grabbedObject = null;
                objectGrabbed = false;
                lineRenderer.enabled = false;
                return;
            }
            Vector3 aimDirection = (aimReticle.transform.position - grabbedObject.transform.position).normalized;
            objectRB.AddForce(aimDirection * (Vector2.Distance(aimReticlePosition, grabbedObjectPosition) * objectMoveSpeed * objectRB.mass));

            //Legacy force calculator
            //objectRB.AddForce(aimDirection * objectMoveSpeed * objectRB.mass);

            lineRenderer.SetPosition(0, grabPathFinder.transform.position);
            lineRenderer.SetPosition(1, grabbedObjectPosition);
        }
    }

    void GrabObject()
    {
        if (_switchInteractMode.interactState == 1 && _vim.firePressed)
        {
            if (!objectGrabbed)
            {
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(aimReticle.transform.position, 0.4f);
                foreach (Collider2D colliderHit in hitColliders)
                {
                    if (_grabbableObjects.Contains(colliderHit.gameObject))
                    {
                        grabbedObject = colliderHit.gameObject;
                        objectRB = colliderHit.GetComponent<Rigidbody2D>();
                        objectRB.gravityScale = 0;
                        objectGrabbed = true;
                        lineRenderer.enabled = true;
                        //Debug.Log("grabbed object successfully");
                    }
                }
            }
        }
        else if (_switchInteractMode.interactState == 1 && !_vim.firePressed)
        {
            if (objectGrabbed)
            {
                objectRB.gravityScale = 1;
                grabbedObject = null;
                objectGrabbed = false;
                lineRenderer.enabled = false;
                //Debug.Log("interact state was grab and fire was not held");
            }
        }
        else if (grabbedObject != null)
        {
            objectRB.gravityScale = 1;
            grabbedObject = null;
            objectGrabbed = false;
            lineRenderer.enabled = false;
            //Debug.Log("interact state was not grab and fire was not held");
        }
    }
}
