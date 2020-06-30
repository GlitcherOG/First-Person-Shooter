using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int teamID;
    RaycastHit hit;
    public GameObject Main;
    public GameObject detail;
    public bool fire;
    public int mag = 10;
    public int magMax = 10;
    public float cooldown = 0.3f;
    public float cooldownTimer = 2f;
    private Controls playerControls;
    private Controls PlayerControls
    {
        get
        {
            if (playerControls != null) return playerControls;
            return playerControls = new Controls();
        }
    }

    [ClientCallback]
    void Update()
    {
        //if Fire button is pushed, fire is false and the cooldown timer is zero
        if (!fire && cooldownTimer <= 0)
        {
            PlayerControls.Player.Fire.performed += ctx => Shoot();
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
        PlayerControls.Player.Reload.performed += ctx => Reload();
    }
    [Mirror.ClientRpc]
    void Shoot()
    {
        //if mag is greater than zero
        if (mag > 0)
        {
            //Minus one from the mag
            mag--;
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
        else
        {
            //Run void reload
            Reload();
        }
    }

    [Mirror.ClientRpc]
    void Reload()
    {
        //New int add
        int add;
        //Minus active magmax from mag
        add = magMax - mag;
        //Check if add is greater than the active ammo
        //Add the add to the mag
        mag += add;
    }
}

