using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack: MonoBehaviour{
    [Header("Stats")]
    [SerializeField] private Sword sword;
    [SerializeField] public float attackCooldown = 0.25f;
    private bool canAttack = true;

    public void OnAttack(InputAction.CallbackContext context){
        if(!context.performed || !canAttack) return;
        StartCoroutine(Attack());
    }

    private IEnumerator Attack(){
        canAttack = false;
        Debug.Log("ATTACKED!");
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}