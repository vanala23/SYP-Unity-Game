using UnityEngine;

[RequireComponent(typeof(EnemyAttack))]
public class Slime: Enemy{
    private EnemyAttack attack;

    protected override void Awake(){
        base.Awake();

        attack = GetComponent<EnemyAttack>();
    }

    protected override bool CanAttack(){
        return attack.CanAttack();
    }

    protected override void DoAttack(){
        attack.Attack();
    }

    protected override void OnMove(Vector2 dir){
        // move animations later
    }

    protected override void OnStop(){
        // idle animations later
    }
}