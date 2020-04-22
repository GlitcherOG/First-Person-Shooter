using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "FPS/Weapons")]
public class Weapons : ScriptableObject
{
    public string weaponName;
    public float damage;
    public int ammo;
    public int maxAmmo;
    public int mag;
    public int magMax;
    public float accuracy;
    public GameObject weaponPrefab;
    public Sprite img;
}
