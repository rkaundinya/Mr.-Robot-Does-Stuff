using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.WasHitByPlayer())
        {
            GameManager.Instance.AddCoin(); 
            Destroy(gameObject); 
        }
    }
}
