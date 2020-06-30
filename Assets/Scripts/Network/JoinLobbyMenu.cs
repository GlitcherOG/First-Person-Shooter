using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;
    [SerializeField] private TMP_InputField ipAddressInputField = null;
    [SerializeField] private Button joinButton = null;
    /// <summary>
    /// Runs at the start of the script
    /// </summary>
    public void Start()
    {
        if(networkManager == null)
        {
            Debug.LogError("networkManager is not attached to JoinLobbyMenu");
        }
        if (landingPagePanel == null)
        {
            Debug.LogError("landingPagePanel is not attached to JoinLobbyMenu");
        }
        if (ipAddressInputField == null)
        {
            Debug.LogError("ipAddressInputField is not attached to JoinLobbyMenu");
        }
        if (joinButton == null)
        {
            Debug.LogError("joinButton is not attached to JoinLobbyMenu");
        }
    }
    /// <summary>
    /// On the scripts being enabled
    /// </summary>
    private void OnEnable()
    {
        networkManager.onClientConnected += HandleClientConnected;
        networkManager.onClientDisconnected += HandleClientDisconnected;
    }
    /// <summary>
    /// When the script is disabled
    /// </summary>
    private void OnDisable()
    {
        networkManager.onClientConnected -= HandleClientConnected;
        networkManager.onClientDisconnected -= HandleClientDisconnected;
    }
    /// <summary>
    /// Makes the player join a lobby
    /// </summary>
    public void JoinLobby()
    {
        string ipAddress = ipAddressInputField.text;

        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();

        joinButton.interactable = false;
    }
    /// <summary>
    /// Handle a client connecting
    /// </summary>
    private void HandleClientConnected()
    {
        joinButton.interactable = true;

        gameObject.SetActive(false);
        landingPagePanel.SetActive(false);
    }
    /// <summary>
    /// Handle a clients dissconnect
    /// </summary>
    private void HandleClientDisconnected()
    {
        joinButton.interactable = true;
    }
}
