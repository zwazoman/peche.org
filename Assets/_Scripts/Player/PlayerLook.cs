using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Camera _playerCamera;

    [Header("Parameters")]
    [SerializeField] float _mouseSensMultiplier = 2;

    float _lookDeltaX;
    float _lookDeltaY;

    float _camRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Look();
    }

    void Look()
    {
        float mouseX = _lookDeltaX * _mouseSensMultiplier * Time.deltaTime;
        float mouseY = _lookDeltaY * _mouseSensMultiplier * Time.deltaTime;

        //mouseX Player rotation
        transform.Rotate(Vector3.up, mouseX);

        //mouseY Camera rotation
        _camRotation -= mouseY;
        _camRotation = Mathf.Clamp(_camRotation, -90f, 90f);
        _playerCamera.transform.localRotation = Quaternion.Euler(_camRotation, 0f, 0f);
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        _lookDeltaX = context.ReadValue<Vector2>().x;
        _lookDeltaY = context.ReadValue<Vector2>().y;
    }
}
