using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerCameraController : NetworkBehaviour
{
    [Header("Camera")]
    [SerializeField] private Vector2 maxFollowOffset = new Vector2(-1f, 6f);
    [SerializeField] private Vector2 cameraVelocity = new Vector2(4f, 0.25f);
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

    private Controls playerControls;
    private Controls PlayerControls
    {
        get
        {
            if (playerControls != null) return playerControls;
            return playerControls = new Controls();
        }
    }
    private CinemachineTransposer transposer;

    /// <summary>
    /// If the player owns this object
    /// </summary>
    public override void OnStartAuthority()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        virtualCamera.gameObject.SetActive(true);

        enabled = true;

        PlayerControls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
    }
    /// <summary>
    /// When the script is enabled
    /// </summary>
    [ClientCallback]
    private void OnEnable() => PlayerControls.Enable();
    /// <summary>
    /// When the script is disabled
    /// </summary>
    [ClientCallback]
    private void OnDisable() => PlayerControls.Disable();
    /// <summary>
    /// Make the camera to look around
    /// </summary>
    /// <param name="lookAxis"></param>
    private void Look(Vector2 lookAxis)
    {
        float followOffset = Mathf.Clamp(
            transposer.m_FollowOffset.y - (lookAxis.y * cameraVelocity.y * Time.deltaTime),
            maxFollowOffset.x,
            maxFollowOffset.y);

        transposer.m_FollowOffset.y = followOffset;

        playerTransform.Rotate(0f, lookAxis.x * cameraVelocity.x * Time.deltaTime, 0f);
    }
}
