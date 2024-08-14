using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    public float rotateSpeed = 180.0f;

    public float jumpForce = 10.0f;

    PlayerInputActions inputActions;

    Rigidbody rb;

    Vector3 direction;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += Move_performed;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= Move_performed;
        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (Time.fixedDeltaTime * moveSpeed * transform.forward * direction.z));
        transform.Rotate(Time.fixedDeltaTime * rotateSpeed * new Vector3(0, direction.x, 0));
    }

    private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector3>();
        rb.AddForce(direction.y * jumpForce * transform.up, ForceMode.Impulse);
    }
}
