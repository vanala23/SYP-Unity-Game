using UnityEngine;

public class EnemyHitbox : MonoBehaviour{
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter2D(Collider2D other){
        PlayerHealth player = other.GetComponentInParent<PlayerHealth>();

        if(player != null){
            Debug.Log("Player hit: " + player.name);
            player.TakeDamage(damage);
        }
    }
}