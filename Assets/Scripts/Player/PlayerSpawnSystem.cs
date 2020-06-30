using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerSpawnSystem : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab = null;

    private static List<Transform> spawnPoints = new List<Transform>();

    private int nextIndex = 0;
    /// <summary>
    /// Adds a spawn point
    /// </summary>
    /// <param name="spawnTransform"></param>
    public static void AddSpawnPoint(Transform spawnTransform)
    {
        spawnPoints.Add(spawnTransform);

        spawnPoints = spawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
    }

    /// <summary>
    /// Static void that removes spawn point
    /// </summary>
    /// <param name="spawnTransform"></param>
    public static void RemoveSpawnPoint(Transform spawnTransform) => spawnPoints.Remove(spawnTransform);
    /// <summary>
    /// On the servers start
    /// </summary>
    public override void OnStartServer()
    {
        NetworkManagerLobby.onServerReadied += SpawnPlayer;
    }
    /// <summary>
    /// On the gameobject destroy
    /// </summary>
    [ServerCallback]
    private void OnDestroy()
    {
        NetworkManagerLobby.onServerReadied -= SpawnPlayer;
    }
    /// <summary>
    /// Spawn a player in
    /// </summary>
    /// <param name="conn"></param>
    [Server]
    public void SpawnPlayer(NetworkConnection conn)
    {
        Transform spawnPoint = spawnPoints.ElementAtOrDefault(nextIndex);

        if(spawnPoint == null)
        {
            Debug.LogError("Missing spawn point for player" + nextIndex);
            return;
        }

        GameObject playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        NetworkServer.Spawn(playerInstance, conn);

        nextIndex++;
    }

}
