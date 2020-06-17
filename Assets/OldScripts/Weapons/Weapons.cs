using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "FPS/Weapons")]
public class Weapons : ScriptableObject
{
    public string weaponName; //Weapon Name
    public float damage; //Damage the weapon does
    public int ammo; //Ammout the weapon currently has
    public int maxAmmo; //Max ammo the weapon can have
    public int mag; //Current mag ammo
    public int magMax; //Max mag ammo
    public float accuracy; //How accurate the gun is
    public GameObject weaponPrefab; //The Prefab used for displaying
    public Sprite img; //Imaged used in the display
}
