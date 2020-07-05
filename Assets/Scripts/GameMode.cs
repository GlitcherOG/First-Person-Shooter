using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    //set up
    public int teamAmmount = 2;
    public static GameMode Instance;
    public List<Team> teams;
    public List<Transform> spawnPoints;

    /// <summary>
    /// Runs at the start of the game
    /// </summary>
    protected void Start()
    {
        Instance = this;
        Debug.Log("Setting up game");
        SetUpGame();
    }

    /// <summary>
    /// Sets up the game for the teams
    /// </summary>
    public void SetUpGame()
    {
        for(int teamID = 0; teamID < teamAmmount; teamID++)
        {
            teams.Add(new Team());
        }
    }
    /// <summary>
    /// Adds score to each of the modes
    /// </summary>
    /// <param name="teamID"></param>
    /// <param name="score"></param>
    public void AddScore(int teamID, int score)
    {
        teams[teamID].score += score;
    }
}

[System.Serializable]
public class Team
{
    public int score;
}