using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UnitMovement : MonoBehaviour {
    [SerializeField] private UnitStatsSO unitStats;

    private float moveSpeed;
    private IUnitMovementController unitController;
    private Vector2 moveDirection;
    private Rigidbody2D rigidBody;

    private void Awake() {
        moveSpeed = unitStats.MoveSpeed;
        rigidBody = GetComponent<Rigidbody2D>();
        unitController = GetComponent<IUnitMovementController>();
    }

    void Update() {
        SetMovementVector();
    }

    private void FixedUpdate() {
        Move();
    }

    private void SetMovementVector() {
        moveDirection = unitController.GetMovementVector();
    }

    private void Move() {
        rigidBody.velocity = moveDirection * moveSpeed;
    }

    public Vector3 GetVelocityVector() {
        return rigidBody.velocity;
    }
}
