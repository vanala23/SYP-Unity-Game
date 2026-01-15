using UnityEngine;

public class Slime : Enemy
{
    [Header("Attack")]
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float attackDuration = 0.6f;

    private float attackTimer;
    private bool isAttacking;
    private float attackTimeLeft;
    private Vector2 attackVelocity;

    private Animator animator;
    private Vector2 lastDirection = Vector2.down;

    protected override void Awake(){
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override void Update(){
        base.Update();

        if(isAttacking) HandleAttackMovement();

        attackTimer -= Time.deltaTime;
    }

    protected override bool CanAttack(){
        if(isAttacking) return false;

        float dist = Vector2.Distance(transform.position, player.position);
        return dist <= attackRange && attackTimer <= 0f;
    }

    protected override void DoAttack(){
        attackTimer = attackCooldown;
        isAttacking = true;
        attackTimeLeft = attackDuration;

        Vector2 dir = (player.position - transform.position).normalized;
        attackVelocity = dir * (Vector2.Distance(transform.position, player.position) / attackDuration);

        lastDirection = dir;

        animator.SetBool("IsMoving", false);
        animator.SetFloat("MoveX", dir.x);
        animator.SetFloat("MoveY", dir.y);
        animator.SetTrigger("Attack");
    }

    private void HandleAttackMovement(){
        attackTimeLeft -= Time.deltaTime;
        rb.linearVelocity = attackVelocity;

        if(attackTimeLeft <= 0f){
            rb.linearVelocity = Vector2.zero;
            isAttacking = false;
        }
    }

    protected override void OnMove(Vector2 dir){
        if(dir != Vector2.zero) lastDirection = dir;

        animator.SetBool("IsMoving", true);
        animator.SetFloat("MoveX", dir.x);
        animator.SetFloat("MoveY", dir.y);
    }

    protected override void OnStop(){
        animator.SetBool("IsMoving", false);
    }

    protected override void OnDrawGizmosSelected(){
        base.OnDrawGizmosSelected();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}