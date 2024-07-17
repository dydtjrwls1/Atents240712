using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 0.01f;

    Vector3 inputDirection = Vector3.zero;

    PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
        playerInputActions.Player.Move.performed += Move;
        playerInputActions.Player.Move.canceled += MoveCanceled;
        playerInputActions.Player.Fire.performed += Fire;
        
    }

    private void OnDisable()
    {
        playerInputActions.Player.Fire.performed -= Fire;
        playerInputActions.Player.Move.canceled -= MoveCanceled;
        playerInputActions.Player.Move.performed -= Move;
        playerInputActions.Disable();
    }

    
    private void Fire(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        Debug.Log("Fire!!");
    }

    private void Move(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        inputDirection = (Vector3)context.ReadValue<Vector2>();
    }



    private void MoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        inputDirection = Vector3.zero;
    }

    private void Update()
    {
        transform.position += (inputDirection * moveSpeed);
    }
}
