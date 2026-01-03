using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement: MonoBehaviour{
    [Header("Movement")]
    public float speed = 4f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastDirection = Vector2.down;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    private void Update(){
        //Inputs
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //fix diagonals
        if(movement.magnitude > 1) movement = movement.normalized;

        //last input
        if(movement != Vector2.zero) lastDirection = movement;
    }

    void FixedUpdate() => rb.linearVelocity = movement * speed;
    public Vector2 GetLastDirection(){return lastDirection;}
}
