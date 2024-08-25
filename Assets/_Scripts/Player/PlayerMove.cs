using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CharacterController controller;

    [Header("Parameters")]
    [SerializeField] float _playerSpeed = 10f;
    [SerializeField] float _sprintMultiplierValue;

    float _sprintMultiplier = 1;
    float x;
    float z;

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * _playerSpeed * _sprintMultiplier * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        x = context.ReadValue<Vector2>().x;
        z = context.ReadValue<Vector2>().y;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            _sprintMultiplier = _sprintMultiplierValue;
        }
        if (context.canceled)
        {
            _sprintMultiplier = 1;
        }
    }
}
