using UnityEngine;

public class Slime: Enemy{
    private EnemyAttack attack;
    private Rigidbody2D rb;

    protected override void Awake(){
        rb = GetComponent<Rigidbody2D>();
        attack = GetComponent<EnemyAttack>();
        currentHP = maxHP;
    }

    protected override void UpdateState(){
        switch(currentState){
            case State.Idle:
                if(hasLOS) currentState = State.Chase;
                break;

            case State.Chase:
                if(!hasLOS){
                    currentState = State.Search;
                    return;
                }

                if(attack.CanAttack()){
                    attack.Attack();
                    return;
                }

                MoveTowards(player.position);
                break;

            case State.Search:
                MoveTowards(lastSeenPosition);
                break;
        }
    }
}