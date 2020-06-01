using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[System.Serializable]
public class Settings : MonoBehaviour
{
    public Text volumeText; //Text used to display the volume
    public Dropdown resolutionDropdown; //Dropdown for the resolution
    public Dropdown quailtyDropdown; //Dropdown for the quailty
    public Slider volumeSlider; //Slider for the volume
    Resolution[] resolutions; //An array used to store the resolutions
    public AudioMixer audioMixer; //The Audio mixer for the volume
    public float volume; //Used to store the volume float
    public KeyCode tempKey, forward, backward, left, right, inventory, interact, jump; //Key codes for all the keys used
    public Text forwardButton, backwardButton, leftButton, rightButton, inventoryButton, interactButton, jumpButton; //Buttons for all the keycodes

    void Start()
    {
        //Get the resolutions the screen can be and set it into the array
        resolutions = Screen.resolutions;
        //Clear all options on the dropdown
        resolutionDropdown.ClearOptions();
        //New list for all the resolution options
        List<string> options = new List<string>();
        //Set the currentResolutionIndex to zero
        int currentResolutionIndex = 0;
        //For all resolutions
        for (int i = 0; i < resolutions.Length; i++)
        {
            //Set the new string option to contain the resoulution width and heigh
            string option = resolutions[i].width + " x " + resolutions[i].height;
            //Add the option to the list
            options.Add(option);
            //if the resolution matches the current resoultion
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                //Set the currentResolutionIndex to i
                currentResolutionIndex = i;
            }
        }
        //Set the quailty dropdown value to be the quality level
        quailtyDropdown.value = QualitySettings.GetQualityLevel();
        //Add the options to resolution
        resolutionDropdown.AddOptions(options);
        //Set the dropdown value to the current resolutionIndex
        resolutionDropdown.value = currentResolutionIndex;
        //Refresh the shown value
        resolutionDropdown.RefreshShownValue();
        //If the save file for settings isnt null
        if (SettingsBinary.LoadSettingsData() != null)
        {
            //Load settings
            Load();
        }
        else
        {
            //Save settings
            Save();
        }
    }

    public void Load()
    {
        //New SettingsData for the data loaded from settings file
        SettingsData data = SettingsBinary.LoadSettingsData();
        //Set the audioMixer volume to the data soundLevel
        audioMixer.SetFloat("Volume", data.soundLevel);
        //Change the volume text to show the volume percentage
        volumeText.text = "Master Volume: " + Mathf.Round((((80f + data.soundLevel) / 80) * 100)).ToString() + "%";
        //Set the resolution dropdown value to the Data resolutionIndex
        resolutionDropdown.value = data.resolutionIndex;
        //Set the quailty dropdown value to the data quailtyIndex
        quailtyDropdown.value = data.quailtyIndex;
        //Set the quality level using the quailtyIndex
        QualitySettings.SetQualityLevel(data.quailtyIndex);
        //Set the volumeSlider value using the data sound level
        volumeSlider.value = data.soundLevel;
        //Set forward basied on data forward
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.forward);
        //Set the forward text to forward
        forwardButton.text = forward.ToString();
        //Set backward basied on data backward
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.backward);
        //Set the backward text to backward
        backwardButton.text = backward.ToString();
        //Set left basied on data left
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.left);
        //Set the left text to left
        leftButton.text = left.ToString();
        //Set right basied on data right
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.right);
        //Set the right text to right
        rightButton.text = right.ToString();
        //Set jump basied on data jump
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.jump);
        //Set the jump text to jump
        jumpButton.text = jump.ToString();
        //Set interact basied on data interact
        interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.interact);
        //Set the interact text to interact
        interactButton.text = interact.ToString();
        //Set inventory basied on data inventory
        inventory = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.inventory);
        //Set the inventory text to inventory
        inventoryButton.text = inventory.ToString();
    }

    public void Forward()
    {
        //If tempKey equals none
        if (tempKey == KeyCode.None)
        {
            //Set the tempKey to forward
            tempKey = forward;
            //Set forward to none
            forward = KeyCode.None;
        }
        //Set the forward text to forward
        forwardButton.text = forward.ToString();
    }

    public void Backward()
    {
        //If tempKey equals none
        if (tempKey == KeyCode.None)
        {
            //Set the tempKey to backward
            tempKey = backward;
            //Set backward to none
            backward = KeyCode.None;
        }
        //Set the backward text to backward
        backwardButton.text = backward.ToString();
    }
    public void Left()
    {
        //If tempKey equals none
        if (tempKey == KeyCode.None)
        {
            //Set the tempKey to left
            tempKey = left;
            //Set left to none
            left = KeyCode.None;
        }
        //Set the left text to left
        leftButton.text = left.ToString();
    }

    public void Right()
    {
        //If tempKey equals none
        if (tempKey == KeyCode.None)
        {
            //Set the tempKey to right
            tempKey = right;
            //Set right to none
            right = KeyCode.None;
        }
        //Set the right text to right
        rightButton.text = right.ToString();
    }
    public void Inventory()
    {
        //If tempKey equals none
        if (tempKey == KeyCode.None)
        {
            //Set the tempKey to inventory
            tempKey = inventory;
            //Set inventory to none
            inventory = KeyCode.None;
        }
        //Set the inventory text to inventory
        inventoryButton.text = inventory.ToString();
    }
    public void Interact()
    {
        //If tempKey equals none
        if (tempKey == KeyCode.None)
        {
            //Set the tempKey to interact
            tempKey = interact;
            //Set interact to none
            interact = KeyCode.None;
        }
        //Set the interact text to interact
        interactButton.text = interact.ToString();
    }
    public void Jump()
    {
        //If tempKey equals none
        if (tempKey == KeyCode.None)
        {
            //Set the tempKey to jump
            tempKey = jump;
            //Set Jump to none
            jump = KeyCode.None;
        }
        //Set the jump text to jump
        jumpButton.text = jump.ToString();
    }
    private void OnGUI()
    {
        //New event e equals the key pressed
        Event e = Event.current;
        //If the tempKey doesnt equal none
        if (tempKey != KeyCode.None)
        {
            //If forward equals none
            if (forward == KeyCode.None)
            {
                //If the key pressed doesn't equal backward
                if (e.keyCode != backward)
                {
                    //Set forward to be key pressed
                    forward = e.keyCode;
                    //Set the forward button text to forward
                    forwardButton.text = forward.ToString();
                }
                else
                {
                    //Set forward to be tempKey
                    forward = tempKey;
                    //Set the forward button text to forward
                    forwardButton.text = forward.ToString();
                }
            }
            else
            //If backward equals none
            if (backward == KeyCode.None)
            {
                //If the key pressed doesn't equal forward
                if (e.keyCode != forward)
                {
                    //Set backward to be key pressed
                    backward = e.keyCode;
                    //Set the backward button text to backward
                    backwardButton.text = backward.ToString();
                }
                else
                {
                    //Set backward to tempKey
                    backward = tempKey;
                    //Set the backward button text to backward
                    backwardButton.text = backward.ToString();
                }
            }
            else
            //If left equals none
            if (left == KeyCode.None)
            {
                //If the key pressed doesn't equal right
                if (e.keyCode != right)
                {
                    //Set left to be key pressed
                    left = e.keyCode;
                    //Set the left button text to left
                    leftButton.text = left.ToString();
                }
                else
                {
                    //Set left to the tempKey
                    left = tempKey;
                    //Set the left button text to left
                    leftButton.text = left.ToString();
                }
            }
            else
            //If right equals none
            if (right == KeyCode.None)
            {
                //If the key pressed doesn't equal left
                if (e.keyCode != left)
                {
                    //Set right to be key pressed
                    right = e.keyCode;
                    //Set the right button text to right
                    rightButton.text = right.ToString();
                }
                else
                {
                    //Set right to tempKey
                    right = tempKey;
                    //Set the right button text to right
                    rightButton.text = right.ToString();
                }
            }
            else
            //If interact equals none
            if (interact == KeyCode.None)
            {
                //Set interact to be key pressed
                interact = e.keyCode;
                //Set the interact button text to interact
                interactButton.text = interact.ToString();
            }
            else
            //If inventory equals none
            if (inventory == KeyCode.None)
            {
                //Set inventory to be key pressed
                inventory = e.keyCode;
                //Set the inventory button text to inventory
                inventoryButton.text = inventory.ToString();
            }
            else
            //If jump equals none
            if (jump == KeyCode.None)
            {
                //Set jump to be key pressed
                jump = e.keyCode;
                //Set the jump button text to jump
                jumpButton.text = jump.ToString();
            }
            else
            {
                //Set tempKey to none
                tempKey = KeyCode.None;
            }
        }
    }

    public void Save()
    {
        //Save settings data
        SettingsBinary.SaveSettingData(this);
    }

    public void SetVolume(float soundLevel)
    {
        //Set the audio mixers volume
        audioMixer.SetFloat("Volume", soundLevel);
        //Change the volume text to show the volume percentage
        volumeText.text = "Master Volume: " + Mathf.Round((((80f + soundLevel) / 80) * 100)).ToString() + "%";
    }

    public void SetFullscreen(bool isFullscreen)
    {
        //Change the fullscreen state using the bool isFullScreen
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuailty(int index)
    {
        //Set the quailty settings using the index
        QualitySettings.SetQualityLevel(index);
    }

    public void SetResolution(int index)
    {
        //Change the resolution of the screen using the array and index
        Resolution resolution = resolutions[index];
    }
}
