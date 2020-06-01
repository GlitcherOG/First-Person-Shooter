using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false; //Is the game paused
    public GameObject pauseMenu; //The pause menu gameobject
    public GameObject SettingsMenu; //Settings gameobject
    public Settings settings; //Settings script

    void Update()
    {
        //If input for escape key and showInv is false
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Toggle pause state
            TogglePause();
        }
    }
    public void TogglePause()
    {
        //If the game is paused
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked; //Locks the cursor
            Cursor.visible = false; //Hides the cursor
            Time.timeScale = 1; //Sets the time scale
            pauseMenu.SetActive(false); //Hides the pause menu gameobject
            SettingsMenu.SetActive(false); //Hides the settings menu gameobject
            isPaused = false; //Changes the bool for being paused
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; //unlocks the cursor
            Cursor.visible = true; //shows the cursor
            Time.timeScale = 0; //Sets the time scale to 0
            pauseMenu.SetActive(true); //shows the menu
            settings.Save(); //Saves the settings
            isPaused = true;//Changes the bool for being paused
        }
    }
}
