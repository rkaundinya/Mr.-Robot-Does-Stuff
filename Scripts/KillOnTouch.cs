using UnityEngine;

public class KillOnTouch : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        var playerMovementController = other.collider.GetComponent<PlayerMovementController>(); 
        if (playerMovementController != null) {
            GameManager.Instance.KillPlayer(); 
        }
    }
}
