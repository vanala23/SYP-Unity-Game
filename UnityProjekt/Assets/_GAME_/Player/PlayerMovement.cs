using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement: MonoBehaviour{
    [Header("Movement")]
    public float speed = 4f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastDirection = Vector2.down;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    public void OnMove(InputAction.CallbackContext context){
        movement = context.ReadValue<Vector2>();

        if(movement.magnitude > 1) movement = movement.normalized;
        if(movement != Vector2.zero) lastDirection = movement;
    }

    private void FixedUpdate() => rb.linearVelocity = movement * speed;
    public Vector2 GetLastDirection(){return lastDirection;}
}
