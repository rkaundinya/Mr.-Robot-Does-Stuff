using UnityEngine;

public class ShellFlipped : MonoBehaviour
{
    [SerializeField]
    private float shellSpeed = 5f; 

    private new Collider2D collider; 
    private new Rigidbody2D rigidbody2D; 

    private Vector2 direction; 

    private void Awake() 
    {
        collider = GetComponent<Collider2D>(); 
        rigidbody2D = GetComponent<Rigidbody2D>(); 
    }

    private void FixedUpdate() 
    {
        rigidbody2D.velocity = new Vector2(direction.x * shellSpeed, rigidbody2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.WasHitByPlayer())
        {
            HandlePlayerCollision(other);
        }
        else 
        {
            if (other.WasSide())
            {
                LaunchShell(other);
                var takeShellHits = other.collider.GetComponent<ITakeShellHits>(); 
                if (takeShellHits != null)
                    takeShellHits.HandleShellHit(this);
            }
                
        }
    }

    private void HandlePlayerCollision(Collision2D other)
    {
        var playerMovementController = other.collider.GetComponent<PlayerMovementController>();

        if (direction.magnitude == 0)
        {
            LaunchShell(other);
            if (other.WasTop())
                playerMovementController.Bounce();
        }
        else
        {
            if (other.WasTop())
            {
                direction = Vector2.zero;
                playerMovementController.Bounce();
            }
            else
            {
                GameManager.Instance.KillPlayer();
            }
        }
    }

    private void LaunchShell(Collision2D other)
    {
        float floatDirection = other.contacts[0].normal.x > 0 ? 
        1f: 
        -1f; 

        direction = new Vector2(floatDirection, 0);
    }
}
