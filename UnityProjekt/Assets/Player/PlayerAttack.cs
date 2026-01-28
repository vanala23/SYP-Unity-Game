using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack: MonoBehaviour{
    [Header("Stats")]
    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform aimSource;

    public void OnAttack(InputAction.CallbackContext context){
        if(!context.performed) return;

        Vector2 direction = (Mouse.current.position.ReadValue() - (Vector2) Camera.main.WorldToScreenPoint(aimSource.position)).normalized;

        weapon.Attack(direction);
    }
}