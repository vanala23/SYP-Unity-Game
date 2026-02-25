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
            animator.SetBool("IsMoving", true);
            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);

            if(movement != Vector2.zero) { lastDirection = movement; }
        }

        if(context.canceled){
            movement = Vector2.zero;
            animator.SetBool("IsMoving", false);
            animator.SetFloat("MoveX", lastDirection.x);
            animator.SetFloat("MoveY", lastDirection.y);
        }
    }


    private void FixedUpdate() => rb.linearVelocity = movement * speed;
    public Vector2 GetLastDirection(){return lastDirection;}
}
