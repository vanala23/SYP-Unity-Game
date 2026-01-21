using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour{
    [SerializeField] private Weapon weapon;
    [SerializeField] private float attackRange = 1.2f;

    private Transform player;

    private void Awake(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public bool CanAttack(){
        return Vector2.Distance(transform.position, player.position) <= attackRange;
    }

    public void Attack(){
        Vector2 direction = (player.position - transform.position).normalized;
        weapon.Attack(direction);
    }
}