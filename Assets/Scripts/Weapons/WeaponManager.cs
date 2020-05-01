using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    RaycastHit hit;
    public Weapons primary;
    public Weapons secondary;
    public int slotActive;
    public GameObject holder;
    public Weapons active;
    public GameObject detail;
    public bool fire;
    public float cooldown = 0.3f;
    public float cooldownTimer = 2f;
    void Update()
    {
        //if Fire button is pushed, fire is false and the cooldown timer is zero
        if (Input.GetButton("Fire1") && !fire && cooldownTimer <= 0)
        {
            //Shoot
            Shoot();
        }
        //If the cooldown timer is less than zero
        if (cooldownTimer > 0)
        {
            //Minus time.delta time from the cooldown
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            //Fire to false
            fire = false;
        }
        //If r is pushed down
        if (Input.GetKeyDown("r"))
        {
            //Reload the gun
            Reload();
        }
        //Update the Ui Ammo with current info
        UIManager.Instance.Ammo.text = "Ammo:" + active.mag + "/" + active.ammo;
    }

    void ChangeWeapon(Weapons temp, bool knife = false)
    {
        //Set fire to true
        fire = true;
        //Change cooldowntimer
        cooldownTimer = 2f;
        //Set active to temp
        active = temp;
        //Destroy gameobject in holder
        Destroy(holder.GetComponentInChildren<GameObject>());
        //Instantiate a new gun
        GameObject temp2 = Instantiate(active.weaponPrefab, holder.transform);
    }

    void Shoot()
    {
        //if mag is greater than zero
        if (active.mag > 0)
        {
            //Minus one from the mag
            active.mag--;
            //Reset the cooldownTimer
            cooldownTimer = cooldown;
            //Set fire to true
            fire = true;
            //Vector to for effect accuracy
            Vector2 RandomShot = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
            //New ray that points out from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0) + new Vector3(RandomShot.x, 0, RandomShot.y));
            //Gets the hitinfo of the raycast and if raycast true
            if (Physics.Raycast(ray, out hit))
            {
                //Instantie a new detail
                GameObject temp = Instantiate(detail, hit.point, hit.transform.rotation);
                //if the Hit transform tag is Enemy
                if (hit.transform.tag == "Enemy")
                {
                    //Damage the Enemy
                    hit.transform.GetComponentInParent<Enemy>().Damage(10f);
                }
            }

        }
        //Else if the ammoi is greater than zero 
        else if (active.ammo > 0)
        {
            //Run void reload
            Reload();
        }
    }

    void Reload()
    {
        //Checks if active gun has ammo
        if(active.ammo>0)
        {
            //New int add
            int add;
            //Minus active magmax from mag
            add = active.magMax - active.mag;
            //Check if add is greater than the active ammo
            if (add <= active.ammo)
            {
                //Minus the add to the ammo
                active.ammo -= add;
                //Add the add to the mag
                active.mag += add;
            }
            else
            {
                //Add the mag to ammo
                active.mag += active.ammo;
                //Set ammo to zero
                active.ammo = 0;
            }
        }
    }

}
