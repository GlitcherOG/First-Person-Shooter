using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class NetworkGamePlayer : NetworkBehaviour
{
    [SyncVar]
    private string displayName = "Loading...";

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
    /// On client start
    /// </summary>
    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);

        Room.GamePlayers.Add(this);
    }
    /// <summary>
    /// On the network destroy
    /// </summary>
    public override void OnNetworkDestroy()
    {
        Room.GamePlayers.Remove(this);
    }
    /// <summary>
    /// Sets the players name
    /// </summary>
    /// <param name="displayName"></param>
    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

}
