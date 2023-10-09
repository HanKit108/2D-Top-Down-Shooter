using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour {
    private Transform player;
    void Start() {
        player = PlayerInstance.Instance.transform;
    }

    void Update() {
        Vector3 targetPosition = (player != null) ? new Vector3(player.position.x, player.position.y, -10f) : transform.position;
        transform.position = targetPosition;
    }
}
