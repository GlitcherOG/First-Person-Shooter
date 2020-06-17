using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance; //Main instance of this script
    public Text Health; //UI Text element for health
    public Text Ammo; //UI Text Element for Ammo
    private void Awake()
    {
        //If the instance is null
        if (Instance == null)
        {
            //Set the instance to this script
            Instance = this;
        }
    }
}