using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureZone : MonoBehaviour
{
    [SerializeField] int teamID;

    public GameModeCTF gameModeCTF;

    /// <summary>
    /// Runs at the start of the game
    /// </summary>
    private void Start()
    {
        gameModeCTF = GameModeCTF.Instance;

        if (gameModeCTF == null)
        {
            Debug.LogError("Could not find GameModeCTF");
        }
    }

    /// <summary>
    /// If something enters the trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test");
        Player player = other.GetComponent<Player>();
        PlayerSingle players = other.GetComponent<PlayerSingle>();
        if (player != null && gameModeCTF != null)
        {
            if (player.teamID != teamID)
            {
                return;
            }

            if (player.Flag)
            {
                gameModeCTF.AddScore(player.teamID, 1);
                player.IsHoldingFlag();
            }
        }
        else if(players != null && gameModeCTF != null)
        {
            if (players.teamID != teamID)
            {
                return;
            }

            if (players.Flag)
            {
                gameModeCTF.AddScore(players.teamID, 1);
                players.IsHoldingFlag();
            }
        }
    }
}
