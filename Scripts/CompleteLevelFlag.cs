using UnityEngine;

public class CompleteLevelFlag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.WasHitByPlayer())
            GameManager.Instance.MoveToNextLevel(); 
    }
}
