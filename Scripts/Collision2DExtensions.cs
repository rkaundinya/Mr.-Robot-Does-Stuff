using UnityEngine;

public static class Collision2DExtensions
{
    public static bool WasHitByPlayer(this Collision2D other)
    {
        return other.collider.GetComponent<PlayerMovementController>() != null;
    }

    public static bool WasHitByPlatform(this Collision2D other)
    {
        return other.gameObject.tag == "Platform"; 
    }

    public static bool WasBottom(this Collision2D other)
    {
        return other.contacts[0].normal.y > 0.5;
    }

    public static bool WasTop(this Collision2D collision)
    {
        return collision.contacts[0].normal.y < -0.5;
    }

    public static bool WasSide (this Collision2D collision)
    {
        return collision.contacts[0].normal.x < -0.5 ||
               collision.contacts[0].normal.x > 0.5;
    }
}