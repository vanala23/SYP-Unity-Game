using UnityEngine;

public class Sword : MonoBehaviour{
    [Header("Stats")]
    [SerializeField] private int damage;

    private Collider2D collider;

    private void Awake(){
        collider = GetComponent<Collider2D>();
        collider.enabled = false;
    }

    public void EnableHitbox(){
        collider.enabled = true;
    }

    public void DisableHitbox(){
        collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.TryGetComponent<Enemy>(out Enemy enemy)) enemy.TakeDamage(damage);
    }
}