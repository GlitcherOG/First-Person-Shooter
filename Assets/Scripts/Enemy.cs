using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f; //Health the Enemy has
    public bool dead; //If the Enemy is dead
    public Animator anim; //The animatior attacted the the enemy
    public GameObject weapon; //Weapon game object on the Enemy

    /// <summary>
    /// Runs every frame for the gamme object
    /// </summary>
    private void Update()
    {
        //If the health is less than or zero and dead is false
        if (health <= 0 && dead == false)
        {
            //Run void death
            Death();
        }
    }
    /// <summary>
    /// Death handling of the enemy
    /// </summary>
    void Death()
    {
        //Set dead to true
        dead = true;
        //Disable the animator
        anim.enabled = false;
        //Destory the Collider
        Destroy(GetComponent<Collider>());
        //Set the parent to Null
        weapon.GetComponentInChildren<Transform>().parent = null;
        //Disable Kinematic
        weapon.GetComponentInChildren<Rigidbody>().isKinematic = false;
    }

    /// <summary>
    /// Void for damaging enemy
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        //Minus damage from health
        health -= damage;
    }

}
