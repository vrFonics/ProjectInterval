using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtCursor : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector3 _aimDirection;

    public GameObject aimReticle;

    public SpriteRenderer gunSpriteRenderer;
    public SpriteRenderer rayGrabSpriteRenderer;

    private float _angle;

    void Start()
    {
        _mainCamera = FindObjectOfType<Camera>();
    }
    
    void Update()
    {
        AimToCursor();
    }

    private void AimToCursor()
    {
        _aimDirection = (aimReticle.transform.position - transform.position).normalized;
        _angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, _angle);
        if (transform.position.x < aimReticle.transform.position.x)
        {
            gunSpriteRenderer.flipY = false;
            rayGrabSpriteRenderer.flipY = false;
        }
        else
        {
            gunSpriteRenderer.flipY = true;
            rayGrabSpriteRenderer.flipY = true;
        }
    }
}
