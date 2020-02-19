using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPlayerPositionOnTouch : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        var playerMovementController = other.collider.GetComponent<PlayerMovementController>(); 
        if (playerMovementController != null) {
            SceneManager.LoadScene(0);
        }
    }
}
