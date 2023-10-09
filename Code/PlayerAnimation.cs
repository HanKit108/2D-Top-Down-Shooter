using UnityEngine;

[RequireComponent(typeof(UnitMovement), typeof(PlayerShoot))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform unitVisual;
    [SerializeField] private Transform aimTransform, aimTransform2;

    [SerializeField] private float angleOffset = 20f;
    private UnitMovement unitMovement;
    private PlayerShoot playerShoot;
    private bool isFacingRight = true;

    private Vector3 closestTarget;

    private void Awake() {
        unitMovement = GetComponent<UnitMovement>();
        playerShoot = GetComponent<PlayerShoot>();
    }

    void Update() {
        animator.SetFloat("Move", unitMovement.GetVelocityVector().magnitude);
        closestTarget = playerShoot.GetClosetTargetPosition();
        Vector3 closestTargetDirection;
        closestTargetDirection = (closestTarget != Vector3.zero) ? closestTarget - transform.position : Vector3.zero;
        if((closestTargetDirection.x < 0 || closestTarget == Vector3.zero && unitMovement.GetVelocityVector().x < 0) && isFacingRight) {
            Flip();
        }
        if((closestTargetDirection.x > 0 || closestTarget == Vector3.zero && unitMovement.GetVelocityVector().x > 0) && !isFacingRight) {
            Flip();
        }
        Aim();
    }

    private void Flip() {
        isFacingRight = !isFacingRight;
        unitVisual.localScale = new Vector3(-unitVisual.localScale.x, unitVisual.localScale.y, unitVisual.localScale.z);
    }

    private void Aim() {
        float angle;
        if(closestTarget != Vector3.zero) {
            Vector3 aimDirection = (closestTarget - transform.position).normalized;
            angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg + angleOffset;
            if(!isFacingRight) {
                angle -= 180f + 2 * angleOffset;
            }
        } else {
            angle = 0f;
        }
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        aimTransform2.eulerAngles = new Vector3(0, 0, angle);
    }
}
