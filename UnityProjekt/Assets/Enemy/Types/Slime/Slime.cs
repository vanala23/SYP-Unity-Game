using Unity.VisualScripting;
using UnityEngine;

public class Slime: Enemy{
    [Header("Attack")]
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float attackDuration = 0.6f;
    [SerializeField] private float searchDuration = 2f;
    private float searchTimer;


    private bool isAttacking;
    private float attackTimeLeft;
    private Vector2 attackVelocity;
    private float attackTimer;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 lastDirection = Vector2.down;


    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected override void updateState(){
        if(isAttacking){
            handleAttackMovement();
            return;
        }
    
        attackTimer -= Time.deltaTime;
    
        switch(currentState){
            case State.Idle:
                animator.SetBool("IsMoving", false);
                if(hasLOS) currentState = State.Chase;
                break;
    
            case State.Chase:
                if(!hasLOS){
                    rb.linearVelocity = Vector2.zero;
                    animator.SetBool("IsMoving", false);
                    currentState = State.Search;
                    return;
                }

                chasePlayer();
                break;
    
            case State.Search:
                searchTimer -= Time.deltaTime;
                moveTowards(lastSeenPosition);

                if(searchTimer <= 0f){
                    rb.linearVelocity = Vector2.zero;
                    animator.SetBool("IsMoving", false);
                    currentState = State.Idle;
                    return;
                }

                if(Vector2.Distance(transform.position, lastSeenPosition) < 0.2f){
                    rb.linearVelocity = Vector2.zero;
                    currentState = State.Idle;
                }
                break;
        }
    }


    private void chasePlayer(){
        float distance = Vector2.Distance(transform.position, player.position);
        if(!hasLOS){
            rb.linearVelocity = Vector2.zero;
            searchTimer = searchDuration;
            currentState = State.Search;
            return;
        }

        if(distance <= attackRange && attackTimer <= 0f){
            Debug.Log("Attack!");
            attack();
            attackTimer = attackCooldown;
            return;
        }

        moveTowards(player.position);
    }

    private void moveTowards(Vector2 target){
        Vector2 direction = (target - (Vector2)transform.position).normalized;

        if(direction != Vector2.zero) lastDirection = direction;

        rb.linearVelocity = direction * moveSpeed;

        animator.SetBool("IsMoving", true);
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
    }

    private void attack(){
        isAttacking = true;
        attackTimeLeft = attackDuration;

        Vector2 start = transform.position;
        Vector2 target = player.position;

        Vector2 dir = (target - start);
        float distance = dir.magnitude;

        if(distance > 0) dir.Normalize();

        attackVelocity = dir * (distance / attackDuration);

        lastDirection = dir;

        animator.SetBool("IsMoving", false);
        animator.SetFloat("MoveX", lastDirection.x);
        animator.SetFloat("MoveY", lastDirection.y);
        animator.SetTrigger("Attack");
    }

    private void handleAttackMovement(){
        attackTimeLeft -= Time.deltaTime;
        rb.linearVelocity = attackVelocity;

        if(attackTimeLeft <= 0f){
            rb.linearVelocity = Vector2.zero;
            isAttacking = false;
        }
    }

    protected override void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(lastSeenPosition, 0.2f);

        Gizmos.color = hasLOS ? Color.yellow : Color.lightBlue;
        Gizmos.DrawLine(transform.position, lastSeenPosition);
    }
}