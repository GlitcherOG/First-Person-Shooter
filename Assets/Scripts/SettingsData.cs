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
        //Set the forward string to be the forward key located in the settings
        forward = settings.forward.ToString();
        //Set the backward string to be the backward key located in the settings
        backward = settings.backward.ToString();
        //Set the left string to be the left key located in the settings
        left = settings.left.ToString();
        //Set the right string to be the right key located in the settings
        right = settings.right.ToString();
        //Set the invenetory string to be the inventory key located in the settings 
        inventory = settings.inventory.ToString();
        //Set the interact string to be the interact key located in the settings
        interact = settings.interact.ToString();
        //Set the jump string to be the jump key located in the settings
        jump = settings.jump.ToString();
    }
}
