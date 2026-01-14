using System;
using UnityEngine;

public abstract class Enemy: MonoBehaviour{
    [Header("References")]
    [SerializeField] protected Transform player;

    [Header("Movement")]
    [SerializeField] protected float moveSpeed;

    [Header("Vision")]
    [SerializeField] protected float viewDistance = 5f;
    [SerializeField] protected LayerMask obstacleMask;

    protected Vector2 lastSeenPosition;
    protected bool hasLOS;
    protected State currentState = State.Idle;

    protected virtual void Update(){
        checkLOS();
        updateState();
    }

    protected void checkLOS(){
        Vector2 origin = transform.position;
        Vector2 target = player.position;
        Vector2 direction = (target - origin).normalized;
        float distance = Vector2.Distance(origin, target);

        if(distance > viewDistance){
            hasLOS = false;
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, obstacleMask);
        Debug.DrawRay(origin, direction * distance, Color.red);
        hasLOS = hit.collider == null;

        if(hasLOS) lastSeenPosition = target;
    }

    protected abstract void OnDrawGizmosSelected();
    
    protected abstract void updateState();

    protected enum State{
        Idle,
        Chase,
        Search
    }
}
