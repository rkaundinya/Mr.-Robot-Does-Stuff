using UnityEngine;

public class CoinBox : MonoBehaviour, ITakeShellHits
{

    [SerializeField]
    private SpriteRenderer enabledSprite; 
    [SerializeField]
    private SpriteRenderer disabledSprite; 
    [SerializeField]
    private int totalCoins = 5;

    public int TotalCoins { get { return totalCoins; } }
    
    private Animator animator;
    private int remainingCoins;

    public void HandleShellHit(ShellFlipped shellFlipped)
    {
        if (remainingCoins > 0)
            TakeCoin(); 
    }

    private void Awake() 
    {
        animator = GetComponent<Animator>(); 
        remainingCoins = totalCoins; 
        enabledSprite.enabled = true; 
        disabledSprite.enabled = false; 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (remainingCoins > 0 &&
            other.WasHitByPlayer() &&
            other.WasBottom())
        {
            TakeCoin();
        }
    }

    private void TakeCoin()
    {
        GameManager.Instance.AddCoin();
        remainingCoins--;
        animator.SetTrigger("CoinFlyUp");

        if (remainingCoins <= 0)
        {
            enabledSprite.enabled = false;
            disabledSprite.enabled = true;
        }
    }
}
