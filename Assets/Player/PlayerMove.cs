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
    public bool doingCoyoteTime;
    public float coyoteTime = 1f;
    public bool once;
    public bool doJump;
    public int jumpCount;
    void Start()
    {
        
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        jump = Input.GetButtonDown("Jump");
        if (jump)
        {
            //controller.jumpCounter += 1;
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
            }
        }

        doJump = doingCoyoteTime || controller.m_Grounded;

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, doJump && jump);
    }

    void endCoyoteTime()
    {
        doingCoyoteTime = false;
    }
}
