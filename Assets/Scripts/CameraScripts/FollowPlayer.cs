using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private PlayerEntityController _player;

    private Camera _mainCamera;

    private Vector3 _aimDirection;

    private AimReticle _aimReticleObject;

    private float _angle;

    [SerializeField]
    private float verticaOffset;
    [SerializeField]
    private float horizontalOffset;
    [SerializeField]
    public float moveTime = 0.5f;
    [SerializeField]
    public float rotateTime = 0.5f;

    private void Start()
    {
        _aimReticleObject = FindObjectOfType<AimReticle>();
        _player = FindObjectOfType<PlayerEntityController>();
        _mainCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        AimToCursor();
        Vector3 currentPosition = transform.position;
        Vector3 playerPosition = _player.transform.position;
        currentPosition = Vector3.Lerp(currentPosition, new Vector3(playerPosition.x, playerPosition.y + verticaOffset, currentPosition.z), moveTime * Time.deltaTime);
    }

    private void AimToCursor()
    {
        Vector3 cameraPosition = _mainCamera.transform.position;
        Vector3 tempVector =  (_aimReticleObject.transform.position - cameraPosition);
        Transform currentTransform = transform;
        tempVector.x = Mathf.Clamp(tempVector.x, cameraPosition.x - horizontalOffset, cameraPosition.x + horizontalOffset);
        _aimDirection = (tempVector - transform.position).normalized;
        _angle = Mathf.LerpAngle(currentTransform.eulerAngles.y, Mathf.Atan2(_aimDirection.x, _aimDirection.z) * Mathf.Rad2Deg, rotateTime * Time.deltaTime);
        currentTransform.eulerAngles = new Vector3(transform.rotation.x, _angle, currentTransform.rotation.z);
    }
}
