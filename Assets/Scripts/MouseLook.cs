using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Player
{
    public class MouseLook : MonoBehaviour
    {
        //Enum for the states of the rotational Axis
        public enum RotationalAxis
        {
            MouseX,
            MouseY
        }
        [Header("Rotation Variavbles")] //Header for the roational values to be used in editor
        public RotationalAxis axis = RotationalAxis.MouseX; //Sets the deafult roational axis
        [Range(0, 200)] //Range for the float
        public float sensitivity = 15; //How sensitive the look speed is
        public float minY = -60, maxY = 60; //Min and max look range for up and down
        private float _rotY; //The y rotation of the camera

        void Start()
        {
            //If the component rigidbody exists
            if (GetComponent<Rigidbody>())
            {
                //Freeze the roatation of the rotation
                GetComponent<Rigidbody>().freezeRotation = true;
            }
            //Lock the cursor state
            Cursor.lockState = CursorLockMode.Locked;
            //Set the visiblitiy of the cursor to false
            Cursor.visible = false;
            //If the componet Camera exists
            if (GetComponent<Camera>())
            {
                //Set the axis to the RotationalAxis mouseY
                axis = RotationalAxis.MouseY;
            }
        }
        void Update()
        {
            //If isPaused is false in the Script PauseMenu
            //if (!PauseMenu.isPaused)
            {
                //If the RotationalAxis is MouseX
                if (axis == RotationalAxis.MouseX)
                {
                    //Rotate the GameObject baised on the sensitivity multiplied by input from Mouse X and deltaTime
                    transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, 0);
                }
                else
                {
                    //Add the Mouse Y multiplied by sensitivity and deltaTime
                    _rotY += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
                    //Clamp the _rotY by the minY and maxY
                    _rotY = Mathf.Clamp(_rotY, minY, maxY);
                    //Transform the Local Rotation with a New Vector3 that uses the y rotation 
                    transform.localEulerAngles = new Vector3(-_rotY, 0, 0);
                }
            }
        }
    }
}
