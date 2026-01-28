using UnityEngine;

public abstract class Weapon: MonoBehaviour{
    [Header("Stats")]
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;

    protected bool canAttack = true;

    public virtual void Attack(Vector2 direction){
        if(!canAttack) return;
        StartCoroutine(AttackRoutine(direction));
    }
    
    protected abstract System.Collections.IEnumerator AttackRoutine(Vector2 direction);
}