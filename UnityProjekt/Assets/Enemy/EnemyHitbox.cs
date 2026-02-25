using UnityEngine;

public class EnemyHitbox : MonoBehaviour{
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Debug.Log("Player hit");

            Enemy enemy = other.GetComponentInParent<Enemy>();

            if(enemy != null){
                Debug.Log("Hit " + enemy.name);
                enemy.TakeDamage(1);
            }
        }
    }
}