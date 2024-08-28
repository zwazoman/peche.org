using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CharacterController controller;
    [SerializeField] Transform _groundCheck;

    [Header("Parameters")]
    [SerializeField] float _playerSpeed = 10f;
    [SerializeField] float _sprintMultiplierValue = 2f;
    [SerializeField] float _groundDistance = 0.4f;
    [SerializeField] LayerMask _groundMask;
    [SerializeField] float _gravityMultiplier = 2f;

    [HideInInspector] public float Gravity = -9.81f;
    [HideInInspector] public bool IsGrounded;

    float _sprintMultiplier = 1;
    float x;
    float z;

    Vector3 velocity;

    private void Awake()
    {
        Gravity = Gravity * _gravityMultiplier;
    }

    void Update()
    {
        IsGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        if (IsGrounded && velocity.y < 0) velocity.y = -2f;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * _playerSpeed * _sprintMultiplier * Time.deltaTime);

        velocity.y += Gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
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
