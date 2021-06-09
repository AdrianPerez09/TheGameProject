using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //turning speed of camera
    public float lookSensitivity = 1;
    public Transform target;
    public Transform player;

    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    public

    //input values from mouse
    float mouseX;
    float mouseY;

    void Start()
    {
        //hiding the cursor during runtime
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        ControlCamera();
    }

    void ControlCamera()
    {
        mouseX += Input.GetAxis("Mouse X") * lookSensitivity;
        mouseY += Input.GetAxis("Mouse Y") * lookSensitivity;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(target);
    
        Vector3 direction = new Vector3(mouseX, 0f, mouseY).normalized;


        //rotate the player
        if (direction.magnitude >= 0.1f)
        {



            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg+target.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            player.rotation = Quaternion.Euler(0, mouseX, 0);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f)*Vector3.forward;
            
        }

        //rotate the camera
        target.rotation = Quaternion.Euler(-mouseY, mouseX, 0);

        
    }
}
