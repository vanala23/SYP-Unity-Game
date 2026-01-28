using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Sword: Weapon{
    private Collider2D hitbox;

    private void Awake(){
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = false;
    }

    protected override IEnumerator AttackRoutine(Vector2 direction){
        canAttack = false;

        transform.right = direction;
        hitbox.enabled = true;
        yield return new WaitForSeconds(0.1f);
        hitbox.enabled = false;

        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.TryGetComponent<Enemy>(out Enemy enemy)) enemy.TakeDamage(damage);
    }
}