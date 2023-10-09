using UnityEngine;

public class PlayerInputs : MonoBehaviour, IUnitMovementController {
    
    [SerializeField] private JoystickHandler joystickHandler;
    [SerializeField] private ShootButtonHandler shootButtonHandler;
    private PlayerControls playerControls;
    private Vector2 direction;
    private bool isShoot;
    //private SaveLoadData saveData = new SaveLoadData();

    private void Awake() {
        playerControls = new PlayerControls();
        playerControls.Player.Enable();
        playerControls.Player.Attack.performed += context => EnableShoot();
        playerControls.Player.Attack.canceled += context => DisableShoot();
    }

    private void Disable() {
        playerControls.Player.Disable();
    }

    public bool IsShoot() {
        if(shootButtonHandler != null && shootButtonHandler.IsShoot()) return true;
        else return isShoot;
    }

    public Vector2 GetMovementVector() {
        if(joystickHandler != null && joystickHandler.GetMovementVector() != Vector2.zero) return joystickHandler.GetMovementVector();
        else return playerControls.Player.Move.ReadValue<Vector2>().normalized;
    }

    private void EnableShoot() {
        isShoot = true;
    }

    private void DisableShoot() {
        isShoot = false;
    }
}
