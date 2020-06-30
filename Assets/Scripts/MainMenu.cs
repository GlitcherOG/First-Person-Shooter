using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;

    /// <summary>
    /// Runs at the start of the game
    /// </summary>
    public void Start()
    {
        if (networkManager == null)
        {
            Debug.LogError("networkManager not attached to MainMenu");
        }

        if (landingPagePanel == null)
        {
            Debug.LogError("landingPagePanel not attached to MainMenu");
        }
    }

    /// <summary>
    /// Runs function if hosting the lobby
    /// </summary>
    public void HostLobby()
    {
        networkManager.StartHost();

        landingPagePanel.SetActive(false);
    }

}
