using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManagerLobby : NetworkManager
{
    [SerializeField] private int minPlayers = 2;
    [Scene][SerializeField] private string menuScene = string.Empty;

    [Header("Room")]
    [SerializeField] private NetworkRoomPlayer roomPlayerPrefab = null;

    [Header("Game")]
    [SerializeField] private NetworkGamePlayer gamePlayerPrefab = null;
    [SerializeField] private GameObject playerSpawnSystem = null;

    public event Action onClientConnected;
    public event Action onClientDisconnected;
    public static event Action<NetworkConnection> onServerReadied;

    public List<NetworkRoomPlayer> RoomPlayers { get; } = new List<NetworkRoomPlayer>();
    public List<NetworkGamePlayer> GamePlayers { get; } = new List<NetworkGamePlayer>();
    /// <summary>
    /// On the sever start
    /// </summary>
    public override void OnStartServer()
    {
        spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList<GameObject>();
    }
    /// <summary>
    /// On the Client start
    /// </summary>
    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        foreach(var prefab in spawnablePrefabs)
        {
            ClientScene.RegisterPrefab(prefab);
        }
    }
    /// <summary>
    /// On the client connect
    /// </summary>
    /// <param name="conn"></param>
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        onClientConnected?.Invoke();
    }
    /// <summary>
    /// On the client disconnect
    /// </summary>
    /// <param name="conn"></param>
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        onClientDisconnected?.Invoke();
    }
    /// <summary>
    /// On the server connect
    /// </summary>
    /// <param name="conn"></param>
    public override void OnServerConnect(NetworkConnection conn)
    {
        if(numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }

        //only if we want people to join only in the lobby
        if(SceneManager.GetActiveScene().path != menuScene)
        {
            conn.Disconnect();
            return;
        }
    }
    /// <summary>
    /// On the server adding a player
    /// </summary>
    /// <param name="conn"></param>
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            bool isLeader = RoomPlayers.Count == 0;

            NetworkRoomPlayer roomPlayerInstance = Instantiate(roomPlayerPrefab);

            roomPlayerInstance.IsLeader = isLeader;

            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
        }
    }

    /// <summary>
    /// On server disconnect
    /// </summary>
    /// <param name="conn"></param>
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if(conn.identity != null)
        {
            NetworkRoomPlayer player = conn.identity.GetComponent<NetworkRoomPlayer>();

            RoomPlayers.Remove(player);

            NotifyPlayersOfReadyState();
        }

        base.OnServerDisconnect(conn);
    }
    /// <summary>
    /// On the server stop
    /// </summary>
    public override void OnStopServer()
    {
        RoomPlayers.Clear();
    }
    /// <summary>
    /// Notify the players of ready state
    /// </summary>
    public void NotifyPlayersOfReadyState()
    {
        foreach (var player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }
    /// <summary>
    /// When everyone is ready to start
    /// </summary>
    /// <returns></returns>
    private bool IsReadyToStart()
    {
        if(numPlayers < minPlayers)
        {
            return false;
        }

        foreach(var player in RoomPlayers)
        {
            if(!player.IsReady)
            {
                return false;
            }
        }

        return true;
    }
    /// <summary>
    /// On the server being ready
    /// </summary>
    /// <param name="conn"></param>

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);

        onServerReadied?.Invoke(conn);
    }
    /// <summary>
    /// Starting game
    /// </summary>
    /// <param name="test"></param>
    public void StartGame(string test = "")
    {
        if(SceneManager.GetActiveScene().path == menuScene)
        {
            if(!IsReadyToStart())
            {
                return;
            }
            if (test == "1")
            {
                ServerChangeScene("Game_Map_01");
            }
            else
            {
                ServerChangeScene("Game_Map_02");
            }
        }
    }

    /// <summary>
    /// On server changing Scene
    /// </summary>
    /// <param name="newSceneName"></param>
    public override void ServerChangeScene(string newSceneName)
    {
        //from menu to game
        if(SceneManager.GetActiveScene().path == menuScene && newSceneName.StartsWith("Game_Map"))
        {
            for(int i = RoomPlayers.Count -1; i >= 0; i--)
            {
                var conn = RoomPlayers[i].connectionToClient;
                NetworkGamePlayer gamePlayerInstance = Instantiate(gamePlayerPrefab);
                gamePlayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);

                NetworkServer.Destroy(conn.identity.gameObject);

                NetworkServer.ReplacePlayerForConnection(conn, gamePlayerInstance.gameObject, true);
            }
        }

        base.ServerChangeScene(newSceneName);
    }
    /// <summary>
    /// Once the servers scene has changed
    /// </summary>
    /// <param name="sceneName"></param>
    public override void OnServerSceneChanged(string sceneName)
    {
        if (sceneName.StartsWith("Game_Map"))
        {
            GameObject playerSpawnSystemInstance = Instantiate(playerSpawnSystem);
            NetworkServer.Spawn(playerSpawnSystemInstance);
        }
    }
}
