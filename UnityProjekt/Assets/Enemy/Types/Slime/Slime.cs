using UnityEngine;

public class Slime : Enemy{
    [Header("Attack")]
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float attackDuration = 0.4f;

    [Header("Hitbox")]
    [SerializeField] private GameObject attackHitbox;

    private float attackTimer;
    private bool isAttacking;
    private float attackTimeLeft;
    private Vector2 attackVelocity;

    private Animator animator;
    private Vector2 lastDirection = Vector2.down;

    protected override void Awake(){
        base.Awake();

        animator = GetComponent<Animator>();

        attackHitbox.SetActive(false);
    }

    protected override void Update(){
        base.Update();

        if(isAttacking)
            HandleAttackMovement();

        attackTimer -= Time.deltaTime;
    }

    protected override bool CanAttack(){
        if(isAttacking)
            return false;

        float dist = Vector2.Distance(transform.position, player.position);

        return dist <= attackRange && attackTimer <= 0f;
    }

    protected override void DoAttack(){
        attackTimer = attackCooldown;

        isAttacking = true;
        attackTimeLeft = attackDuration;

        Vector2 dir = (player.position - transform.position).normalized;

        lastDirection = dir;

        attackVelocity = dir * (attackRange / attackDuration);

        animator.SetBool("IsMoving", false);
        animator.SetFloat("MoveX", dir.x);
        animator.SetFloat("MoveY", dir.y);
        animator.SetTrigger("Attack");

        attackHitbox.SetActive(true);
    }

    private void HandleAttackMovement(){
        rb.linearVelocity = attackVelocity;

        attackTimeLeft -= Time.deltaTime;

        if(attackTimeLeft <= 0f){
            isAttacking = false;

            rb.linearVelocity = Vector2.zero;
            attackHitbox.SetActive(false);
        }
    }

    protected override void OnMove(Vector2 dir){
        if(dir != Vector2.zero)
            lastDirection = dir;

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