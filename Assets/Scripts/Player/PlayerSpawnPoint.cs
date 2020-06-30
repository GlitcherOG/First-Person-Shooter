using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    /// <summary>
    /// On script awake add spawnpoint
    /// </summary>
    private void Awake()
    {
        PlayerSpawnSystem.AddSpawnPoint(transform);
    }
    /// <summary>
    /// On script destory
    /// </summary>
    private void OnDestroy()
    {
        PlayerSpawnSystem.RemoveSpawnPoint(transform);
    }
    /// <summary>
    /// Draw the spawn point on the editor window
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.3f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 1f);
    }
}
