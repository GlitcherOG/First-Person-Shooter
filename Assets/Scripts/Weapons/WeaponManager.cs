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
    private void Start()
    {

    }
    void Update()
    {
        if (Input.GetButton("Fire1") && !fire && cooldownTimer <= 0)
        {
            Shoot();
        }
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            fire = false;
        }
        if(Input.GetKeyDown("r"))
        {
            Reload();
        }
        UIManager.Instance.Ammo.text = "Ammo:" + active.mag + "/" + active.ammo;
    }

    void ChangeWeapon(Weapons temp, bool knife = false)
    {
        fire = true;
        cooldownTimer = 2f;
        active = temp;
        Destroy(holder.GetComponentInChildren<GameObject>());
        GameObject temp2 = Instantiate(active.weaponPrefab, holder.transform);
    }

    void Shoot()
    {
        if (active.mag > 0)
        {
            active.mag--;
            cooldownTimer = cooldown;
            fire = true;
            Vector2 RandomShot = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
            //New ray that points out from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0) + new Vector3(RandomShot.x, 0, RandomShot.y));
            //Gets the hitinfo of the raycast
            //Physics.Raycast(ray, out hit);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject temp = Instantiate(detail, hit.point, hit.transform.rotation);
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponentInParent<Enemy>().Damage(10f);
                }
            }

        }
        else if (active.ammo > 0)
        {
            Reload();
        }
    }

    void Reload()
    {
        if(active.ammo>0)
        {
            int add;
            add = active.magMax - active.mag;
            if (add <= active.ammo)
            {
                active.ammo -= add;
                active.mag += add;
            }
            else
            {
                active.mag += active.ammo;
                active.ammo = 0;
            }
        }
    }

}
