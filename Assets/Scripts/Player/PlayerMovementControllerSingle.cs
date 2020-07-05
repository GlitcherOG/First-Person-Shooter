using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Examples.Basic;

public class PlayerMovementControllerSingle : MonoBehaviour
{
    [Header("Speed Vars")]
    public float moveSpeed; //Public float for the current movement speed of the character
    public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed; //walk speed, run speed, crouch speed and jump speed of the character
    private float _gravity = 20; //The ammount of gravity applyed to the character
    private Vector3 _moveDir; //Vector3 for the move direction of the character
    private CharacterController _charC; //The Character Controller used for movement 

    private void Start()
    {
        //Get the component CharacterController and set it into the Refrence _charC
        _charC = GetComponent<CharacterController>();
    }
    private void Move()
    {
        //If the character controller is grounded
        if (_charC.isGrounded)
        {
             //Change the movement speed to the walkspeed
            moveSpeed = walkSpeed;
            //Apply a new transform direction to the Vector3 using the Horizontal movement keys Multiplied by movement speed, and the Vertical Axis keys also Multiplied by the moveSpeed 
            _moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0, (Input.GetAxis("Vertical") * moveSpeed)));
        }
        //Minus gravity Multiplied by deltaTime from the vertical Y value of moveDir
        _moveDir.y -= _gravity * Time.deltaTime;
        //Change the movement of the Character controller to use moveDir Multiplied by deltaTime
        _charC.Move(_moveDir * Time.deltaTime);
    }
    private void Update()
    {
        //Start the Move void
        Move();
    }
}
