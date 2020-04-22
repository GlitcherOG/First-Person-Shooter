using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    private Rigidbody rigd;
    public float moveSpeed;
    public float runSpeed;
    public float jump;
    public GameObject model;
    float distToGround;
    public Animator anim;
    bool IsGrounded
    {
        get { return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f); }
    }

private void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
        rigd = GetComponent<Rigidbody>();
        foreach (Rigidbody rb in model.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
        foreach (Collider rb in model.GetComponentsInChildren<Collider>())
        {
            rb.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            rigd.velocity += new Vector3(0, jump, 0);
        }
        Vector3 speed = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rigd.velocity.y, (Input.GetAxis("Vertical") * moveSpeed));
        if (Input.GetButton("Run") && Input.GetButton("Vertical"))
        {
            anim.SetBool("Running", true);
            anim.SetBool("Walking", false);
            speed += new Vector3(runSpeed, 0, 0);
        }
        else if (Input.GetButton("Vertical") && !Input.GetButton("Run"))
        {
            anim.SetBool("Walking", true);
            anim.SetBool("Running", false);
        }
        else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
        }
        rigd.velocity = transform.TransformDirection(speed);
    }
}
