using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Changes the game scene using the scene index
    /// </summary>
    /// <param name="sceneIndex"></param>
    public void ChangeScene(int sceneIndex)
    {
        //Load the scene locatated at scene Index
        SceneManager.LoadScene(sceneIndex);
    }
    /// <summary>
    /// Exits the game
    /// </summary>
    public void ExitGame()
    {
        //If the player is the unity editor
#if UNITY_EDITOR
        //Set the unity editor to stop playing
        UnityEditor.EditorApplication.isPlaying =
            false;
#else //Else
        //Quit the application
        Application.Quit();
#endif
    }
}
