using UnityEngine;

public class EnemyAI : MonoBehaviour, IUnitMovementController
{
    private Transform target;
    [SerializeField] private UnitStatsSO unitStats;
    [SerializeField] private EnemyAIValues enemyAIValues;

    private float attackRange, attactkCooldown, damage, chaseRange, leaveRange;

    private enum State {
        Roaming,
        ChaseTarget,
    }

    private State state;
    private Vector3 startPosition, targetPosition;
    private float reachedPositionDistance = 2f;
    private bool canAttack;
    
    private void Awake() {
        state = State.Roaming;
        damage = unitStats.Damage;
        attackRange = unitStats.AttackRange;
        attactkCooldown = unitStats.AttackCooldown;

        chaseRange = enemyAIValues.ChaseRange;
        leaveRange = enemyAIValues.LeaveRange;

        target = PlayerInstance.Instance.transform;
    }

    void Start() {
        startPosition = transform.position;
        targetPosition = GetRoamingPosition();
        RefreshAttack();
    }

    void Update() {
        if(target != null) {
        switch (state) {
            default:
            case State.Roaming:
                RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPosition);
                if(Vector3.Distance(transform.position, targetPosition) < reachedPositionDistance || (hit.collider != null && hit.collider.gameObject != this.gameObject)) {
                    targetPosition = GetRoamingPosition();
                }
                Debug.DrawLine(transform.position, targetPosition, Color.white);
                
                FindTarget();
                break;
            case State.ChaseTarget:
                LeaveTarget();
                Debug.DrawLine(transform.position, targetPosition, Color.red);
                if(Vector3.Distance(transform.position, target.position) < attackRange) {
                    Attack();
                }
                else {
                    targetPosition = target.position;
                }
                break;
        } 
        } else targetPosition = transform.position;
    }

    private Vector3 GetRoamingPosition() {
        return startPosition + GetRandomDirection() * Random.Range(2f, 10f);
    }

    public Vector2 GetMovementVector() {
        Vector3 moveDir = targetPosition - transform.position;
        return new Vector2(moveDir.x, moveDir.y).normalized;
    }

    private Vector3 GetRandomDirection() {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void FindTarget() {
        if(Vector3.Distance(transform.position, target.position) < chaseRange) {
            state = State.ChaseTarget;
        }
    }
    private void LeaveTarget() {
        if(Vector3.Distance(transform.position, target.position) > leaveRange) {
            state = State.Roaming;
        }
    }

    private void Attack() {
        if(canAttack) {
            FunctionTimer.Create(RefreshAttack, attactkCooldown);
            canAttack = false;

            UnitHealth unit = target.gameObject.GetComponent<UnitHealth>();
            if(unit != null){
                unit.TakeDamage(damage);
            }
        }
    }

    private void RefreshAttack() {
        canAttack = true;
    }
}
