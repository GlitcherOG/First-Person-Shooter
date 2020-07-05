using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponManagerSingle : MonoBehaviour
{
    public int teamID;
    RaycastHit hit;
    public GameObject Main;
    public GameObject detail;
    public Animator anim;
    public Text text;
    public bool fire;
    public int mag = 10;
    public int magMax = 10;
    public bool aim;
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
    /// <summary>
    /// Waits for inputs
    /// </summary>
    void Update()
    {
        text.text = mag.ToString() + "/" + magMax.ToString();
            if (fire && cooldownTimer <= 0 && Input.GetMouseButtonDown(0)) 
            {
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
                //Fire to true
                fire = true;
            }
        if (Input.GetKeyDown("r"))
        {
            Reload();
        }
        if(Input.GetMouseButtonDown(1))
        {
            aim = !aim;
            anim.SetBool("Aim", aim);
        }
    }
    /// <summary>
    /// Shoots the weapon across all clients
    /// </summary>
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
                if (hit.transform.tag == "Player")
                {
                    //Damage the Enemy
                    hit.transform.GetComponentInParent<Player>().damage(10f);
                }
            }

        }
        else
        {
            //Empty
            Reload();
        }
    }

    /// <summary>
    /// Shoots the weapon across all clients
    /// </summary>
    //[Client]
    void Reload()
    {
        anim.SetTrigger("Reload");
        //New int add
        int add;
        //Minus active magmax from mag
        add = magMax - mag;
        //Check if add is greater than the active ammo
        //Add the add to the mag
        mag += add;
    }
}

