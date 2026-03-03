using UnityEngine;

public abstract class Enemy : MonoBehaviour{
    [Header("References")]
    [SerializeField] protected Transform player;

    [Header("Stats")]
    [SerializeField] protected int maxHP = 3;
    [SerializeField] protected int attackPower = 1;

    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 2f;

    [Header("Vision")]
    [SerializeField] protected float viewDistance = 5f;
    [SerializeField] protected LayerMask obstacleMask;

    [Header("Search")]
    [SerializeField] protected float searchDuration = 2f;

    protected int currentHP;
    protected bool hasLOS;
    protected Vector2 lastSeenPosition;

    protected Rigidbody2D rb;

    protected State currentState = State.Idle;
    protected float searchTimer;

    protected enum State{
        Idle,
        Chase,
        Search
    }

    protected virtual void Awake(){
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;

        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if(player == null){
            Debug.LogError($"{name} cannot find Player!");
            enabled = false;
        }

        Debug.Log("Player ref: " + player);
    }

    protected virtual void Update(){
        CheckLOS();
        UpdateState();
    }

    #region STATE C

    protected virtual void UpdateState(){
        switch(currentState){
            case State.Idle:
                OnIdle();
                break;

            case State.Chase:
                OnChase();
                break;

            case State.Search:
                OnSearch();
                break;
        }
    }

    protected virtual void OnIdle(){
        Stop();

        if(hasLOS)
            currentState = State.Chase;
    }

    protected virtual void OnChase(){
        if(!hasLOS){
            searchTimer = searchDuration;
            currentState = State.Search;
            return;
        }

        if(CanAttack()){
            DoAttack();
            return;
        }

        MoveTowards(player.position);
    }

    protected virtual void OnSearch(){
        if(hasLOS){
            currentState = State.Chase;
            return;
        }

        searchTimer -= Time.deltaTime;

        MoveTowards(lastSeenPosition);

        if(searchTimer <= 0f || Vector2.Distance(transform.position, lastSeenPosition) < 0.2f){
            currentState = State.Idle;
        }
    }

    #endregion

    #region MOVEMENT

    protected void MoveTowards(Vector2 target){
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;

        OnMove(dir);
    }

    protected void Stop(){
        rb.linearVelocity = Vector2.zero;

        OnStop();
    }

    #endregion

    #region VISION

    protected void CheckLOS(){
        if(player == null) return;

        Vector2 origin = transform.position;
        Vector2 target = player.position;

        float distance = Vector2.Distance(origin, target);

        if(distance > viewDistance){
            hasLOS = false;
            return;
        }

        Vector2 dir = (target - origin).normalized;
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, distance, obstacleMask);

        hasLOS = hit.collider == null;

        if(hasLOS)
            lastSeenPosition = target;

        Debug.DrawRay( origin, dir * distance, hasLOS ? Color.green : Color.red);
    }

    #endregion

    #region DAMAGE

    public virtual void TakeDamage(int amount){
        currentHP -= amount;

        if (currentHP <= 0)
            Die();
    }

    protected virtual void Die(){
        Destroy(gameObject);
    }

    #endregion

    #region HOOKS

    protected virtual bool CanAttack() => false;
    protected virtual void DoAttack(){}
    protected virtual void OnMove(Vector2 dir){}
    protected virtual void OnStop(){}

    #endregion

    protected virtual void OnDrawGizmosSelected(){
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(lastSeenPosition, 0.2f);
    }
}