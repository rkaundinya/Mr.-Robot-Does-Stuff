using System.Collections;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private LayerMask layerMask;  

    private bool useRaycast = true; 
    private new Rigidbody2D rigidbody2D; 
    
    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        if (useRaycast)
        {
            checkIfPlayerIsBelow(transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        AudioManager.Instance.Stop("Attacking Spike"); 
        StartCoroutine("WaitToDestroy");
    }

    private void OnDestroy() {
        AudioManager.Instance.Stop("Attacking Spike");
    }

    private void checkIfPlayerIsBelow(Transform spikePosition)
    {
        var raycastHit = Physics2D.Raycast(spikePosition.position, Vector2.down, maxDistance, layerMask);

        if (raycastHit.collider != null && 
            raycastHit.collider.GetComponent<PlayerMovementController>() != null)
        {
            rigidbody2D.simulated = true; 
            AudioManager.Instance.Play("Attacking Spike"); 
            useRaycast = false; 
        }
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
