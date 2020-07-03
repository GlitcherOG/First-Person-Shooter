using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class NetworkRoomPlayer : NetworkBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject lobbyUI = null;
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[4];
    [SerializeField] private TMP_Text[] playerReadyTexts = new TMP_Text[4];
    [SerializeField] private Button startGameButton = null;


    [SyncVar(hook = nameof(HandleDisplayNameChanged))]
    public string DisplayName = "Loading...";
    [SyncVar(hook = nameof(HandleReadyStatusChanged))]
    public bool IsReady = false;

    private bool isLeader = false;
    public bool IsLeader
    {
        set
        {
            isLeader = value;
            if (startGameButton != null)
            {
                startGameButton.gameObject.SetActive(value);
            }
        }
    }

    private NetworkManagerLobby room;
    private NetworkManagerLobby Room
    {
        get
        {
            if( room != null)
            {
                return room;
            }
            room = NetworkManager.singleton as NetworkManagerLobby;
            return room;
        }
    }
    /// <summary>
    /// If the player owns the object and script
    /// </summary>
    public override void OnStartAuthority()
    {
        CmdSetDisplayName(PlayerNameInput.DisplayName);

        lobbyUI.SetActive(true);
    }
    /// <summary>
    /// On the client start
    /// </summary>
    public override void OnStartClient()
    {
        Room.RoomPlayers.Add(this);
        UpdateDisplay();
    }
    /// <summary>
    /// On network destory 
    /// </summary>
    public override void OnNetworkDestroy()
    {
        Room.RoomPlayers.Remove(this);
        UpdateDisplay();
    }
    /// <summary>
    /// Changing the display name
    /// </summary>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    public void HandleDisplayNameChanged(string oldValue, string newValue)
    {
        UpdateDisplay();
    }
    /// <summary>
    /// Change the handle ready status from the player
    /// </summary>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    public void HandleReadyStatusChanged(bool oldValue, bool newValue)
    {
        UpdateDisplay();
    }
    /// <summary>
    /// Update the display
    /// </summary>
    private void UpdateDisplay()
    {
        if(!hasAuthority)
        {
            this.gameObject.SetActive(false);
            foreach(var player in Room.RoomPlayers)
            {
                if(player.hasAuthority)
                {
                    player.UpdateDisplay();
                    break;
                }
            }
            return;
        }

        for(int i = 0; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = "Waiting for Player...";
            playerReadyTexts[i].text = string.Empty;
        }

        for(int i = 0; i < Room.RoomPlayers.Count; i++)
        {
            playerNameTexts[i].text = Room.RoomPlayers[i].DisplayName;
            playerReadyTexts[i].text = Room.RoomPlayers[i].IsReady ?
                    "<color=green>Ready</color>" :
                    "<color=red>Not Ready</color>";
        }
    }
    /// <summary>
    /// Handle the ready 
    /// </summary>
    /// <param name="readyToStart"></param>
    public void HandleReadyToStart(bool readyToStart)
    {
        if(!isLeader)
        {
            return;
        }

        startGameButton.interactable = readyToStart;
    }
    /// <summary>
    /// Set the display name
    /// </summary>
    /// <param name="displayName"></param>
    [Command]
    private void CmdSetDisplayName(string displayName)
    {
        DisplayName = displayName;
    }
    /// <summary>
    /// Ready up on the server
    /// </summary>
    [Command]
    public void CmdReadyUp()
    {
        IsReady = !IsReady;

        Room.NotifyPlayersOfReadyState();
    }
    /// <summary>
    /// Start the Game
    /// </summary>
    [Command]
    public void CmdStartGame()
    {
        if(connectionToClient != Room.RoomPlayers[0].connectionToClient)
        {
            return;
        }

        Room.StartGame();
    }
}
