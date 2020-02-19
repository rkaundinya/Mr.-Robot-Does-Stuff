using UnityEngine;

public static class Collider2DExtensions
{
    public static bool WasHitByPlayer(this Collider2D other)
    {
        return other.GetComponent<PlayerMovementController>() != null;
    }
}
