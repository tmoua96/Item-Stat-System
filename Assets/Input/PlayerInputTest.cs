using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputTest : MonoBehaviour
{
    private PlayerCharacterActions actions;
    [SerializeField] 
    private float moveSpeed = 3;

    private Vector2 moveInput;

    private void OnEnable()
    {
        actions = new PlayerCharacterActions();
        actions.Enable();

        actions.Player.Move.performed += OnMovePerformed;
        actions.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        actions.Disable();

        actions.Player.Move.performed -= OnMovePerformed;
        actions.Player.Move.canceled -= OnMoveCanceled;
    }

    private void Update()
    {
        //Vector2 moveInput = actions.FindAction("Move").ReadValue<Vector2>();
        //moveInput = actions.Player.Move.ReadValue<Vector2>();

        if (actions.Player.Jump.IsPressed())
        {
            // Jump()
        }

        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.deltaTime;

        transform.position += movement;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        moveInput = Vector2.zero;
    }
}
