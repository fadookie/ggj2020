using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40;

    float horizontalInput;
    float horizontalMove;
    bool jump;
    Animator anim;
    Rigidbody2D rb;
    public bool doingCoyoteTime;
    public float coyoteTime = 1f;
    public bool once;
    public bool doJump;
    public int jumpCount;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        anim.SetFloat("VelX", Mathf.Abs(Input.GetAxis("Horizontal")));
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        jump = Input.GetButtonDown("Jump");
        if (jump)
        {
            //controller.jumpCounter += 1;
            anim.Play("Jump");
        }
        if (!controller.m_Grounded)
        {
            if (!once)
            {
                once = true;
                Invoke("endCoyoteTime", coyoteTime);
            }
        }
        else
        {
            doingCoyoteTime = true;
        }

        if (controller.m_Grounded)
        {
            if (once)
            {
                once = false;
                controller.jumpCounter = 0;
                anim.Play("Land");
            }
        }

        if(rb.velocity.y < 0)
        {
            anim.Play("Fall");
        }

        doJump = doingCoyoteTime || controller.m_Grounded;

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, doJump && jump);
    }

    void endCoyoteTime()
    {
        doingCoyoteTime = false;
    }
}
