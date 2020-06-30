using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    //Game mode
    [SerializeField] int playersTeamID;
    public int teamID{ get { return playersTeamID; } }
    public static Player Instance;
    //weapons
    public List<Weapon> weapons;
    int currentWeapon = 0;
    int lastWeapon = 0;
    public float forwardDropOffset;
    public float upDropOffset;

    public override void OnStartAuthority()
    {
        SwitchWeapon(currentWeapon);
        Instance = this;
    }
    [ClientCallback]
    private void Update()
    {

    }
    [Client]
    public void PickUpWeapon(GameObject weaponObject, Vector3 originalLocation, int teamID, int weaponID, bool overrideLock = false)
    {
        SwitchWeapon(weaponID, overrideLock);

        weapons[weaponID].SetWeaponGameObject(teamID, weaponObject, originalLocation);
    }
    [Client]
    public void SwitchWeapon(int weaponID, bool overrideLock = false)
    {
        if(!overrideLock && weapons[currentWeapon].isWeaponLocked == true)
        {
            return;
        }

        lastWeapon = currentWeapon;
        currentWeapon = weaponID;

        foreach (Weapon weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }

        weapons[currentWeapon].gameObject.SetActive(true);
    }

    [Client]
    public void DropWeapon(int weaponID)
    {
        if (weapons[weaponID].isWeaponDropable)
        {
            Vector3 forward = transform.forward;
            forward.y = 0;
            forward *= forwardDropOffset;
            forward.y = upDropOffset;
            Vector3 dropLocation = transform.position + forward;

            weapons[weaponID].DropWeapon(this.gameObject, dropLocation);
            weapons[weaponID].worldWeaponGameObject.SetActive(true);


            SwitchWeapon(lastWeapon,true);//if possible
        }
    }
    [Client]
    public void ReturnWeapon(int weaponID)
    {
        if (weapons[weaponID].isWeaponDropable)//flag
        {
            Vector3 returnLocation = weapons[weaponID].originalLocation;

            weapons[weaponID].worldWeaponGameObject.transform.position = returnLocation;
            weapons[weaponID].worldWeaponGameObject.SetActive(true);

            SwitchWeapon(lastWeapon,true);//if possible
        }
    }
    [Client]
    //bad
    public bool IsHoldingFlag()
    {
        if(currentWeapon == 1)
        { 
            return true;
        }

        return false;
    }
    

    public int GetWeaponTeamID()
    {
        return weapons[currentWeapon].teamID;
    }
}
