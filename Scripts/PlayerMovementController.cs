using System;
using UnityEngine;

[RequireComponent(typeof(CharacterGrounding))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour, IMove
{
    [SerializeField]
    private float moveSpeed = 2;
    [SerializeField]
    private float jumpForce = 400;
    [SerializeField]
    private float wallJumpForce = 350; 

    // We use new here because rigidbody2D is an obsolete call to the current
    // object's rigidbody component and you want to tell Unity here that 
    // we mean to create a new rigidbody2D object and know what we're doing
    private new Rigidbody2D rigidbody2D; 
    private CharacterGrounding characterGrounding;

    public float Speed { get; private set; }

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>(); 
        characterGrounding = GetComponent<CharacterGrounding>(); 
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1") && characterGrounding.IsGrounded) {
            rigidbody2D.AddForce(Vector2.up * jumpForce); 

            if (characterGrounding.GroundedDirection != Vector2.down)
            {
                rigidbody2D.AddForce(characterGrounding.GroundedDirection * -1f * wallJumpForce); 
            }
        }
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal"); 
        Speed = horizontal; 

        Vector3 movement = new Vector3(horizontal, 0); 

        transform.position += movement * Time.deltaTime * moveSpeed;
    }

    internal void Bounce()
    {
        rigidbody2D.AddForce(Vector2.up * jumpForce); 
    }
}
