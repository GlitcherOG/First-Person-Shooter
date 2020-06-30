using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Examples.Basic;

public class PlayerMovementController : NetworkBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private CharacterController controller = null;

    private Vector2 previousInput;

    private Controls playerControls;

    private Controls PlayerControls
    {
        get
        {
            if (playerControls != null) return playerControls;
            return playerControls = new Controls();
        }
    }

    /// <summary>
    /// If the player owns this object/script
    /// </summary>
    public override void OnStartAuthority()
    {
        enabled = true;

        PlayerControls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
        PlayerControls.Player.Move.canceled += ctx => ResetMovement();
    }

    /// <summary>
    /// On script Enable
    /// </summary>
    [ClientCallback]
    private void OnEnable() => PlayerControls.Enable();
    /// <summary>
    /// On script Disable
    /// </summary>
    [ClientCallback]
    private void OnDisable() => PlayerControls.Disable();
    /// <summary>
    /// Every frame update
    /// </summary>
    [ClientCallback]
    private void Update()
    {
        Move();
    }
    /// <summary>
    /// Sets the movement of the player
    /// </summary>
    /// <param name="movement"></param>
    [Client]
    private void SetMovement(Vector2 movement)
    {
        previousInput = movement;
    }
    /// <summary>
    /// Resets the movement of the controller
    /// </summary>
    [Client]
    private void ResetMovement()
    {
        previousInput = Vector2.zero;
    }
    /// <summary>
    /// Moves the character
    /// </summary>
    [Client]
    private void Move()
    {
        Vector3 right = controller.transform.right;
        Vector3 forward = controller.transform.forward;

        right.y = 0f;
        forward.y = 0f;

        Vector3 movement = right.normalized * previousInput.x
            + forward.normalized * previousInput.y;

        controller.Move(movement * movementSpeed * Time.deltaTime);
    }
}
