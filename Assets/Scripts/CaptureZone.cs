using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureZone : MonoBehaviour
{
    [SerializeField] int teamID;

    GameModeCTF gameModeCTF;

    /// <summary>
    /// Runs at the start of the game
    /// </summary>
    private void Start()
    {
        gameModeCTF = FindObjectOfType<GameModeCTF>();

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
        Player player = other.GetComponent<Player>();

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
    }
}
