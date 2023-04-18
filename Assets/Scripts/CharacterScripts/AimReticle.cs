using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimReticle : MonoBehaviour
{
    public Camera mainCamera;
    private Vector3 _mousePosition;
    Vector3 _mousePositionLastFrame;

    public float reticleSpeed;

    public PlayerInput playerInput;

    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }

    private void Start()
    {
        Cursor.visible = false;
        
        _mousePositionLastFrame = GetMousePosition();
    }

    private void UpdateMousePosition()
    {
        _mousePosition = GetMousePosition();
    }
    private Vector3 GetMousePosition()
    {
        //return new Vector3(playerInput.actions.FindAction("Reticle").ReadValue<Vector2>().x, playerInput.actions.FindAction("Reticle").ReadValue<Vector2>().y, 0 - mainCamera.transform.position.z);
        return mainCamera.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0 - mainCamera.transform.position.z));
    }

    private void SwitchControllerUsedLast()
    {
        Vector2 recticleVector = VirtualInputManager.Instance.reticleVector;
        if (playerInput.currentControlScheme.Equals("Keyboard and Mouse"))
        {
            transform.position = _mousePosition;
        }
        else
        {
            Vector3 moveVector = new Vector3(recticleVector.x, recticleVector.y, 0f);
            transform.position += (moveVector * (reticleSpeed * Time.deltaTime));
        }
    }

    void Update()
    {
        UpdateMousePosition();
        SwitchControllerUsedLast();
    }
}
