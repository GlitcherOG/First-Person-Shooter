using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    private Rigidbody rigd; //Rigidbody for the player
    public float moveSpeed; //Movement speed
    public float runSpeed; //Running speed
    public float jump; //Jumping hight
    public GameObject model; //Players model
    float distToGround; //Distance to the ground 
    public float heatlh = 100f; //Float health for the player
    public Animator anim; //Animator for the player
    bool IsGrounded
    {
        get { return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f); }
    }

private void Start()
    {
        //Get the distance to ground and extend the bounds
        distToGround = GetComponent<Collider>().bounds.extents.y;
        //Get the rigidbody
        rigd = GetComponent<Rigidbody>();
        //For each rididbody in children 
        foreach (Rigidbody rb in model.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
        //For each collider in child disable
        foreach (Collider rb in model.GetComponentsInChildren<Collider>())
        {
            rb.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //    //If jump is true and isgrounded is true
        //    if (Input.GetButtonDown("Jump") && IsGrounded)
        //    {
        //        //Add jump to the rigid Velcity
        //        rigd.velocity += new Vector3(0, jump, 0);
        //    }
        //    //Get speed form horizontal times move speed and vertical speed times move speed
        //    Vector3 speed = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rigd.velocity.y, (Input.GetAxis("Vertical") * moveSpeed));
        //    //If running button is true and movemnt vertical is also true
        //    if (Input.GetButton("Run") && Input.GetButton("Vertical"))
        //    {
        //        //Set the running to true and walking to false
        //        anim.SetBool("Running", true);
        //        anim.SetBool("Walking", false);
        //        //Add run speed to the vector speed
        //        speed += new Vector3(runSpeed, 0, 0);
        //    }
        //    //If the vertical is active but not running
        //    else if (Input.GetButton("Vertical") && !Input.GetButton("Run"))
        //    {
        //        //Set walking to true and running to false
        //        anim.SetBool("Walking", true);
        //        anim.SetBool("Running", false);
        //    }
        //    else
        //    {
        //        //Set walking to false and running to true
        //        anim.SetBool("Walking", false);
        //        anim.SetBool("Running", false);
        //    }
        //    //Transform the rigid body velocity
        //    rigd.velocity = transform.TransformDirection(speed);
        //    //Change the health on the text UI
        //    UIManager.Instance.Health.text = "Heath:" + heatlh.ToString();
    }
}
