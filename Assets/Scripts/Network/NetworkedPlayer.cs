using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedPlayer : NetworkBehaviour
{
    [SerializeField] private Vector3 movement = new Vector3();

    /// <summary>
    /// On every frame update
    /// </summary>
    [Client]
    void Update()
    {
        if (!hasAuthority)
        { 
            return; 
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CmdMove();
        }
    }
    /// <summary>
    /// Move command
    /// </summary>
    [Command]
    private void CmdMove()
    {
        RpcMove();
    }
    /// <summary>
    /// Translate the player
    /// </summary>
    [ClientRpc]
    private void RpcMove() => transform.Translate(movement);

}
