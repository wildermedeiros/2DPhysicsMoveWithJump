using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float walkForce = 200f;
    [SerializeField] float jumpForce = 50f;
    [SerializeField] float fallGravity = 10f;  
    [SerializeField] LayerMask groundLayerMask;
    bool jumpRequested = false;
    Vector2 axisValues;
    Rigidbody2D myRigidbody2D;

    private void Awake() 
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();    
    }

    private void FixedUpdate()
    {
        Move();

        // if(Grounded())
        // {
        //     Jump();
        // }

        ApplyIntensityOnFallJump();
    }

    private bool Grounded()
    {
        if (Physics2D.IsTouchingLayers(GetComponent<BoxCollider2D>(), groundLayerMask))
        {
            return true;
        }
        return false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        axisValues = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Jump();
            //jumpRequested = true;
        }
    }

    private void ApplyIntensityOnFallJump()
    {
        if (myRigidbody2D.velocity.y < -0.01f)
        {
            myRigidbody2D.gravityScale = fallGravity * Time.fixedDeltaTime;
        }
        else
        {
            myRigidbody2D.gravityScale = 1f;
        }
    }

    private void Move()
    {
        if(axisValues.sqrMagnitude < 0.01f) { return; }

        var newForce = new Vector2(axisValues.x * walkForce, 0f);

        myRigidbody2D.AddForce(newForce);
    }

    private void Jump()
    {
        if (Grounded())
        {
            myRigidbody2D.velocity = Vector2.up * jumpForce * Time.fixedDeltaTime;
            jumpRequested = false;
        }

        // if(jumpRequested)
        // {
        //     myRigidbody2D.velocity = Vector2.up * jumpForce * Time.fixedDeltaTime;
        //     jumpRequested = false;
        // }
    }
}
