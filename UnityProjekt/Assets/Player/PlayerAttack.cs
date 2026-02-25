using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour{
    [Header("Attack")]
    public float attackDuration = 0.15f;
    public float cooldown = 0.3f;

    [SerializeField] private GameObject swordHitbox;
    private Animator animator;
    private PlayerMovement movement;
    private bool canAttack = true;

    private void Awake(){
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();

        swordHitbox.SetActive(false);
    }

    public void OnAttack(InputAction.CallbackContext context){
        if(!context.performed) return;
        if(!canAttack) return;

        Vector2 dir = movement.GetLastDirection();

        StartCoroutine(AttackRoutine(dir));
    }

    private IEnumerator AttackRoutine(Vector2 dir){
        canAttack = false;

        movement.enabled = false;

        animator.SetFloat("MoveX", dir.x);
        animator.SetFloat("MoveY", dir.y);
        animator.SetTrigger("Attack");

        swordHitbox.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        swordHitbox.SetActive(false);

        movement.enabled = true;

        yield return new WaitForSeconds(cooldown);

        canAttack = true;
    }
}