using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack: MonoBehaviour{
    public float duration = 0.15f;
    public float distance = 0.8f;
    public LayerMask enemyLayer;

    private PlayerMovement movement;
    private bool isAttacking;

    void Awake() => movement = GetComponent<PlayerMovement>();

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Space) && !isAttacking) StartCoroutine(Attack());

    }

    System.Collections.IEnumerator Attack(){
        isAttacking = true;

        Vector2 dir = movement.GetLastDirection();
        Vector2 origin = (Vector2)transform.position + dir * distance;

        Collider2D hit = Physics2D.OverlapCircle(origin, 0.4f, enemyLayer);
        if(hit) Debug.Log("Enemy hit: " + hit.name);

        yield return new WaitForSeconds(duration);
        isAttacking = false;
    }

    private void OnDrawGizmosSelected(){
        if(!movement) return;

        Gizmos.color = Color.red;
        Vector2 dir = movement.GetLastDirection();
        Vector2 origin = (Vector2)transform.position + dir * distance;
        Gizmos.DrawWireSphere(origin, 0.4f);
    }
}
