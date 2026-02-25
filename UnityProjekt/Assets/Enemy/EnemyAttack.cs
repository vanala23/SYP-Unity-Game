using UnityEngine;

public class EnemyAttack: MonoBehaviour{
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float cooldown = 1f;

    private Transform player;

    private bool canAttack = true;

    private void Awake(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public bool CanAttack(){
        if(!canAttack) return false;

        return Vector2.Distance(transform.position, player.position) <= attackRange;
    }

    public void Attack(){
        if (!canAttack) return;

        StartCoroutine(AttackRoutine());
    }

    private System.Collections.IEnumerator AttackRoutine(){
        canAttack = false;

        Debug.Log("Enemy attacked player");

        // damage for player later

        yield return new WaitForSeconds(cooldown);

        canAttack = true;
    }
}