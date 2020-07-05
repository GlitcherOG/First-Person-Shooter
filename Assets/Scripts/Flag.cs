using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] int teamID;
    public Vector3 originalLocation;

    private void Start()
    {
        originalLocation = transform.position;
    }

    /// <summary>
    /// On a trigger enter
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        var players = other.GetComponent<PlayerSingle>();
        if (player != null)
        {//its a player
            if (player.teamID == teamID)
            {//cant pick up your own team's flag

                //return flag
                return;
            }

            Debug.Log("Capture Flag");

            player.IsHoldingFlag(this.gameObject);

        }
        else if(players != null)
        {
            if (players.teamID == teamID)
            {//cant pick up your own team's flag

                //return flag
                return;
            }

            Debug.Log("Capture Flag");

            players.IsHoldingFlag(this.gameObject);
        }
    }
}
