using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement: MonoBehaviour{
    [Header("Movement")]
    public float speed = 4f;

    private Rigidbody2D rb;
    private Vector2 movement, lastDirection;
    private Animator animator;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context){
        movement = context.ReadValue<Vector2>();

        if(context.performed){
            animator.SetBool("IsWalking", true);
            animator.SetFloat("InputX", movement.x);
            animator.SetFloat("InputY", movement.y);

            if(movement != Vector2.zero) { lastDirection = movement; }
        }

        if(context.canceled){
            movement = Vector2.zero;
            animator.SetBool("IsWalking", false);
            animator.SetFloat("LastInputX", lastDirection.x);
            animator.SetFloat("LastInputY", lastDirection.y);
        }
    }


    private void FixedUpdate() => rb.linearVelocity = movement * speed;
    public Vector2 GetLastDirection(){return lastDirection;}
}
