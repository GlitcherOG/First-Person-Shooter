[System.Serializable]
public class SettingsData
{
    public int quailtyIndex; //The quailty Index of the game
    public int resolutionIndex; //resolution Index of the game
    public float soundLevel; //Sound Level of the game
    public string forward, backward, left, right, inventory, interact, jump; //Strings containing the forward, backward, left, right, inventory, interact and jump keys

    public SettingsData(Settings settings)
    {
        //Set the quailty index to be the value located from the quailty dropdown 
        quailtyIndex = settings.quailtyDropdown.value;
        //Set the resoulution index to be the value located in the resolution dropdown menu
        resolutionIndex = settings.resolutionDropdown.value;
        //Set the soundlevel to the the value located in the volumeSlider
        soundLevel = settings.volumeSlider.value;
    }
}
