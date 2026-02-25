using UnityEngine;

public class SwordHitbox : MonoBehaviour{
    private void OnTriggerEnter2D(Collider2D other){
        if(other.TryGetComponent<Enemy>(out Enemy enemy)){
            Debug.Log("Hit " + enemy.name);
            enemy.TakeDamage(1);
        }
    }
}