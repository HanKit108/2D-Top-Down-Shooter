using UnityEngine;

[RequireComponent(typeof(UnitMovement))]
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform unitVisual;
    private UnitMovement unitMovement;
    private bool isFacingRight = true;


    private void Awake() {
        unitMovement = GetComponent<UnitMovement>();
    }

    void Update() {
        animator.SetFloat("Move", unitMovement.GetVelocityVector().magnitude);

        if(unitMovement.GetVelocityVector().x < 0 && isFacingRight) {
            Flip();
        }
        if(unitMovement.GetVelocityVector().x > 0 && !isFacingRight) {
            Flip();
        }
    }

    private void Flip() {
        isFacingRight = !isFacingRight;
        unitVisual.localScale = new Vector3(-unitVisual.localScale.x, unitVisual.localScale.y, unitVisual.localScale.z);
    }
}
