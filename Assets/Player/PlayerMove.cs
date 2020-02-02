using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController2D controller;

    float horizontalInput;
    float horizontalMove;
    bool jump;
    public bool doingCoyoteTime;
    public float coyoteTime = 1f;
    public bool once;
    public bool doJump;
    public int jumpCount;
    
    void Start() {
        controller = GetComponent<CharacterController2D>();
    }

    private void Update() {
        var movementComponent = GetComponent<PlayerMovementComponent>();

        if (!movementComponent) return;
        
        if (movementComponent.jump)
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

        controller.Move(movementComponent.horizontalMove * Time.fixedDeltaTime, false, doJump && movementComponent.jump);
    }

    void endCoyoteTime()
    {
        doingCoyoteTime = false;
    }
}
