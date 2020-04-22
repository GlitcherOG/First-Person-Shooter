using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public bool dead;
    public Animator anim;
    public GameObject weapon;
    private void Update()
    {
        if (health <= 0 && dead == false)
        {
            Death();
        }
    }

    void Death()
    {
        dead = true;
        anim.enabled = false;
        Destroy(GetComponent<Collider>());
        weapon.GetComponentInChildren<Transform>().parent = null;
        weapon.GetComponentInChildren<Rigidbody>().isKinematic = false;
    }
    public void Damage(float damage)
    {
        health -= damage;
    }

}
