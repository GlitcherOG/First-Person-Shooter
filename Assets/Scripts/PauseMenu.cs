using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false; //Is the game paused
    public GameObject pauseMenu; //The pause menu gameobject
    public GameObject SettingsMenu; //Settings gameobject
    public Settings settings; //Settings script

    private Controls playerControls;

    private Controls PlayerControls
    {
        get
        {
            if (playerControls != null) return playerControls;
            return playerControls = new Controls();
        }
    }

    void Update()
    {
        //If input for escape key and showInv is false
        PlayerControls.Player.Pause.performed += ctx => TogglePause();
    }

    /// <summary>
    /// Toggles the game pause menu
    /// </summary>
    public void TogglePause()
    {
        //If the game is paused
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked; //Locks the cursor
            Cursor.visible = false; //Hides the cursor
            pauseMenu.SetActive(false); //Hides the pause menu gameobject
            SettingsMenu.SetActive(false); //Hides the settings menu gameobject
            isPaused = false; //Changes the bool for being paused
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; //unlocks the cursor
            Cursor.visible = true; //shows the cursor
            pauseMenu.SetActive(true); //shows the menu
            settings.Save(); //Saves the settings
            isPaused = true;//Changes the bool for being paused
        }
    }
}
