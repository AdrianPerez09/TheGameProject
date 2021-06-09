using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public Animator animator;
    public CapsuleCollider collider;
    public Transform actualPosition;
    public float speed ;
    public float walkSpeed=0.2f;
    public float runningSpeed = 0.42f;
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
        bool fall = false;

        if (IsGrounded())
        {
            Debug.Log("Player on the ground");
            
            groundHeight= actualPosition.transform.position.y;
            

                animator.SetBool("is_grounded", true);
                animator.SetBool("is_falling", false);
            
        }
        else
        {

             airHeight=actualPosition.transform.position.y;

            
                fall = true;
         
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
            else if(Input.GetKey(KeyCode.LeftShift) && !animator.GetBool("is_colliding") && fall)
            {

                animator.SetBool("is_falling", true);
                animator.SetBool("is_running", false);

            }else

            {
                speed = walkSpeed;

                animator.SetBool("is_running", false);
                animator.SetBool("is_walking", true);

                if (animator.GetBool("is_colliding"))
                {
                    animator.SetBool("is_walking", false);

                }

                if (fall)
                {
                    animator.SetBool("is_falling", true);
                    animator.SetBool("is_walking", false);

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

            if (fall)
            {
                animator.SetBool("is_falling", true);
                animator.SetBool("is_walkingLeft", false);

            }


        }
        else if (horizontal >= 1)
        {
            animator.SetBool("is_walkingLeft", false);
            animator.SetBool("is_walkingRight", true);
            speed = walkSpeed;
            if (fall)
            {
                animator.SetBool("is_falling", true);
                animator.SetBool("is_walkingRight", false);

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

   

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("collition detected");


        animator.SetBool("is_colliding", true);

    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);

    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("collition avoided");
        animator.SetBool("is_colliding", false);
    }


}
