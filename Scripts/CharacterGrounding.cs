using System;
using UnityEngine;

public class CharacterGrounding : MonoBehaviour
{
    [SerializeField]
    private Transform[] positions; 

    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private LayerMask layerMask;

    private Transform groundedObject; 

    // ? tells language that vector3 could also be null
    private Vector3? groundedObjectLastPosition; 

    // Use the get; set; in curly braces because you want to set specific
    // access settings to a public bool variable 
    // get means that anything can get this property and private set means that 
    // only this script can set if this is grounded
    public bool IsGrounded { get; private set; }
    public Vector2 GroundedDirection { get; private set; }

    // Update is called once per frame
    private void FixedUpdate()
    {
        foreach (var position in positions)
        {
            CheckFootForGrounding(position);
            if (IsGrounded)
                break; 
        }

        StickToMovingObjects(); 
    }

    private void CheckFootForGrounding(Transform foot)
    {
        var raycastHit = Physics2D.Raycast(foot.position, foot.forward, maxDistance, layerMask);
        Debug.DrawRay(foot.position, foot.forward * maxDistance, Color.red);
        
        if (raycastHit.collider != null)
        {
            if (groundedObject != raycastHit.collider.transform)
            {
                groundedObject = raycastHit.collider.transform; 
                IsGrounded = true;
                groundedObjectLastPosition = groundedObject.position; 
            }
            GroundedDirection = foot.forward; 
        }
        else
        {
            groundedObject = null; 
            IsGrounded = false;
        }
    }

    private void StickToMovingObjects()
    {
        if (groundedObject != null)
        {
            if (groundedObjectLastPosition.HasValue &&
                groundedObjectLastPosition.Value != groundedObject.position)
            {
                Vector3 delta = groundedObject.position - groundedObjectLastPosition.Value; 
                transform.position += delta; 
            }
            groundedObjectLastPosition = groundedObject.position; 
        }

        else 
        {
            groundedObjectLastPosition = null; 
        }
    }
}
