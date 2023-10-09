using UnityEngine;

[RequireComponent(typeof(PlayerInputs), typeof(Inventory))]
public class PlayerShoot : MonoBehaviour {
    
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private UnitStatsSO unitStats;

    private float attackRange, damage, attackCooldown;
    private PlayerInputs playerInputs;
    private bool canShoot = false;
    private Inventory inventory;

    private void Awake() {
        attackRange = unitStats.AttackRange;
        damage = unitStats.Damage;
        attackCooldown = unitStats.AttackCooldown;
        playerInputs = GetComponent<PlayerInputs>();
        inventory = GetComponent<Inventory>();
    }

    void Start() {
        RefreshShoot();
    }

    void Update() {
        if(playerInputs.IsShoot()) {
            Shoot();
        }
    }

    public Vector3 GetClosetTargetPosition() {
        RaycastHit2D closestHit = GetClosestHitInRange();
        Vector3 closetTargetPosition = (closestHit.collider != null) ? closestHit.collider.transform.position : closetTargetPosition = Vector3.zero;
        return closetTargetPosition;
    }

    private void Shoot() {
        if(canShoot) {
            RaycastHit2D closestHit = GetClosestHitInRange();
            if(closestHit.collider != null) {
                FunctionTimer.Create(RefreshShoot, attackCooldown);

                canShoot = false;
                UnitHealth unit = closestHit.collider.gameObject.GetComponent<UnitHealth>();
                if(unit != null) {
                    unit.TakeDamage(damage);
                }
            }
        }
    }

    public RaycastHit2D GetClosestHitInRange() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackRange, Vector3.zero, attackRange, enemyLayer);
        RaycastHit2D closestHit = new RaycastHit2D();
        if(hits.Length > 0) {
            closestHit = hits[0];
        }

        foreach(RaycastHit2D hit in hits) {
            float currentDistance = Vector3.Distance(hit.collider.transform.position, transform.position);
            float closestDistance = Vector3.Distance(closestHit.collider.transform.position, transform.position);
            if(currentDistance < closestDistance) {
                closestHit = hit;
            }
        }
        return closestHit;
    }

    private void RefreshShoot() {
        canShoot = inventory.SpendBullet();
    }
}
