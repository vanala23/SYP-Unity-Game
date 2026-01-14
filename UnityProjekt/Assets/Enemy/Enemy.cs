using System;
using UnityEngine;

public abstract class Enemy: MonoBehaviour{
    [Header("References")]
    [SerializeField] protected Transform player;

    [Header("Stats")]
    [SerializeField] protected int maxHP;
    [SerializeField] protected int attackPower;

    [Header("Movement")]
    [SerializeField] protected float moveSpeed;

    [Header("Vision")]
    [SerializeField] protected float viewDistance = 5f;
    [SerializeField] protected LayerMask obstacleMask;

    protected int currentHP;
    protected Vector2 lastSeenPosition;
    protected bool hasLOS;
    protected State currentState = State.Idle;

    protected virtual void Update(){
        CheckLOS();
        UpdateState();
    }

    protected void CheckLOS(){
        Vector2 origin = transform.position;
        Vector2 target = player.position;
        Vector2 direction = (target - origin).normalized;
        float distance = Vector2.Distance(origin, target);

        if(distance > viewDistance){
            hasLOS = false;
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, obstacleMask);
        hasLOS = !hit;

        Debug.DrawRay(origin, direction * distance, Color.red);

        if(hasLOS) lastSeenPosition = target;
    }

    public virtual void TakeDamage(int amount){
        currentHP -= amount;

        if(currentHP <= 0) Die();
    }

    protected virtual void Die(){
        Destroy(gameObject);
    }


    protected abstract void OnDrawGizmosSelected();
    
    protected abstract void UpdateState();

    protected enum State{
        Idle,
        Chase,
        Search
    }
}
