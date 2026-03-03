using UnityEngine;

public class SwordHitbox : MonoBehaviour{
    private void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Hit object: " + other.name);

        Enemy enemy = other.GetComponentInParent<Enemy>();

        if(enemy != null){
            Debug.Log("Enemy found: " + enemy.name);
            enemy.TakeDamage(1);
        }
    }
}