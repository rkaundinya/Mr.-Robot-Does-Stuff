﻿using System;
using UnityEngine;

public class Walker : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f; 
    [SerializeField]
    private GameObject spawnOnStompPrefab; 

    private new Collider2D collider; 
    private new Rigidbody2D rigidbody2D;
    private Vector2 direction = Vector2.left;
    private SpriteRenderer sprite; 

    private void Awake() 
    {
        collider = GetComponent<Collider2D>(); 
        rigidbody2D = GetComponent<Rigidbody2D>(); 
        sprite = GetComponent<SpriteRenderer>(); 
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.WasHitByPlayer())
        {
            if (other.WasTop())
                HandleWalkerStomp(other.collider.GetComponent<PlayerMovementController>());
            else 
                GameManager.Instance.KillPlayer(); 
        }
    }

    private void HandleWalkerStomp(PlayerMovementController playerMovementController)
    {
        if (spawnOnStompPrefab != null) 
        {
            Instantiate(spawnOnStompPrefab, transform.position, transform.rotation);
        }
        
        if (spawnOnStompPrefab.GetComponent<ShellFlipped>() == null)
            playerMovementController.Bounce(); 

        Destroy(gameObject);
    }

    private void FixedUpdate() 
    {
        rigidbody2D.MovePosition(rigidbody2D.position + direction 
            * speed * Time.fixedDeltaTime);
    }

    private void LateUpdate() 
    {
        if (ReachedEdge() || HitNonPlayer())
        {
            SwitchDirections(); 
        }
    }

    private bool ReachedEdge()
    {
        float x = GetForwardX();

        float y = collider.bounds.min.y;

        Vector2 origin = new Vector2(x, y);

        var hit = Physics2D.Raycast(origin, Vector2.down, 0.1f);

        if (hit.collider == null)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    private bool HitNonPlayer()
    {
        float x = GetForwardX();
        float y = transform.position.y; 

        Vector2 origin = new Vector2(x, y);
        Debug.DrawRay(origin, direction * 0.1f);

        var hit = Physics2D.Raycast(origin, direction, 0.1f);        

        if (hit.collider == null ||
            hit.collider.isTrigger ||
            hit.collider.GetComponent<PlayerMovementController>() != null)
        {
            return false; 
        }

        return true; 
    }

    private float GetForwardX()
    {
        return direction.x == -1 ?
                    collider.bounds.min.x - 0.1f :
                    collider.bounds.max.x + 0.1f;
    }

    private void SwitchDirections()
    {
        direction *= -1; 
        sprite.flipX = !sprite.flipX; 
    }
}
