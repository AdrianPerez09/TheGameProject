using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public Animator animator;
    public Transform actualPosition;
    public float speed ;
    public float walkSpeed=0.1f;
    public float runningSpeed = 0.2f;
    public float rotationSpeed=200.0f;
    public float turnSmothTime = 0.1f;
    public float airHeight=0;
    public float groundHeight=0;
    



    float turnSmothVelocity;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        

        setMovement(animator,horizontal,vertical);

    }

    private void setMovement(Animator animator,float horizontal,float vertical)
    {
        bool falling = false;

        if (IsGrounded())
        {
            Debug.Log("Player on the ground");
            
           
            

                animator.SetBool("is_grounded", true);
                animator.SetBool("is_falling", false);
            
        }
        else
        {

             

            fallIdle();

            
         
            Debug.Log("Player on the air.");
        }


        if (vertical != 0 )
        {



            if (Input.GetKey(KeyCode.LeftShift) && !animator.GetBool("is_colliding"))
            {
                speed = runningSpeed;

                animator.SetBool("is_running", true);
                animator.SetBool("is_walking", false);
            }
            else if(Input.GetKey(KeyCode.LeftShift) && !animator.GetBool("is_colliding")|| animator.GetBool("is_colliding") &&animator.GetBool("is_falling"))
            {

                fallRun();


            }
            else

            {
                speed = walkSpeed;

                animator.SetBool("is_running", false);
                animator.SetBool("is_walking", true);

                if (animator.GetBool("is_colliding"))
                {
                    animator.SetBool("is_walking", false);

                }

                if (animator.GetBool("is_falling"))
                {
                    fallWalk();

                }


            }
        }
        else
        {
            animator.SetBool("is_walking", false);
            animator.SetBool("is_running", false);



        }

        if (horizontal <= -1)
        {
            animator.SetBool("is_walkingLeft", true);
            animator.SetBool("is_walkingRight", false);
            speed = walkSpeed;

            if (animator.GetBool("is_falling"))
            {
                fallLeft();

            }


        }
        else if (horizontal >= 1)
        {
            animator.SetBool("is_walkingLeft", false);
            animator.SetBool("is_walkingRight", true);
            speed = walkSpeed;
            if (animator.GetBool("is_falling"))
            {
                fallRight();

            }
        }
        else
        {

            animator.SetBool("is_walkingLeft", false);
            animator.SetBool("is_walkingRight", false);

        }


        if (horizontal >= 1 && vertical >= 1)
        {
            animator.SetBool("is_walkingLeft", false);
            animator.SetBool("is_walkingRight", false);
            animator.SetBool("is_running", false);
            animator.SetBool("is_walking", true);
            speed = walkSpeed;

        }

        if (horizontal <= -1 && vertical >= 1)
        {
            animator.SetBool("is_walkingLeft", false);
            animator.SetBool("is_walkingRight", false);
            animator.SetBool("is_running", false);
            animator.SetBool("is_walking", true);
            speed = walkSpeed;


        }

    }

    private void fallLeft()
    {

        animator.SetBool("is_grounded", false);
        animator.SetBool("is_falling", true);
        animator.SetBool("is_walking", false);
        animator.SetBool("is_walkingRight", false);
        animator.SetBool("is_walkingLeft", true);
        animator.SetBool("is_running", false);

    }

    private void fallRight()
    {

        animator.SetBool("is_grounded", false);
        animator.SetBool("is_falling", true);
        animator.SetBool("is_walking", false);
        animator.SetBool("is_walkingRight", true);
        animator.SetBool("is_walkingLeft", false);
        animator.SetBool("is_running", false);


    }

    private void fallWalk()
    {
        animator.SetBool("is_grounded", false);
        animator.SetBool("is_falling", true);
        animator.SetBool("is_walking", true);
        animator.SetBool("is_walkingRight", false);
        animator.SetBool("is_walkingLeft", false);
        animator.SetBool("is_running", false);
    }

    private void fallRun()
    {
        animator.SetBool("is_grounded", false);
        animator.SetBool("is_falling", true);
        animator.SetBool("is_walking", false);
        animator.SetBool("is_walkingRight", false);
        animator.SetBool("is_walkingLeft", false);
        animator.SetBool("is_running", true);
    }

    private void fallIdle()
    {

        animator.SetBool("is_grounded", false);
        animator.SetBool("is_falling", true);
        animator.SetBool("is_walking", false);
        animator.SetBool("is_walkingRight", false);
        animator.SetBool("is_walkingLeft", false);
        animator.SetBool("is_running", false);
    }

    private void OnTriggerEnter(Collider obj)
    {

        if (obj.tag!="Item")
        {

            Debug.Log("collition detected");


            animator.SetBool("is_colliding", true);
        }
    }
    private bool IsGrounded()
    {
     

        return Physics.Raycast(transform.position, Vector3.down,0.7f );

    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("collition avoided");
        animator.SetBool("is_colliding", false);
    }


}
