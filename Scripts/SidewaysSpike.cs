using System.Collections;
using UnityEngine;

public class SidewaysSpike : MonoBehaviour
{
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private LayerMask layerMask; 
    [SerializeField]
    private float spikeSpeed = 300;
    [SerializeField]
    private bool rightSpike, leftSpike;

    private Vector2 raycastCheckDirection; 
    private new Rigidbody2D rigidbody2D; 

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>(); 
        rigidbody2D.simulated = false; 
    }

    private void Start() {
        if (rightSpike) 
        {
            raycastCheckDirection = Vector2.left; 
        }

        if (leftSpike) 
        {
            raycastCheckDirection = Vector2.right; 
        }
    }

    void Update()
    {
        CheckIfPlayerIsInSight(transform); 
    }

    private void OnDestroy() {
        AudioManager.Instance.Stop("Attacking Spike");
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.GetComponent<SidewaysSpike>() != null ||
            other.collider.GetComponent<FallingSpike>() != null)
        {
            AudioManager.Instance.Play("Spike Collision");
            Destroy(gameObject);
        }

        if (!other.WasHitByPlayer() && 
            !other.WasHitByPlatform())
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
            MoveSpike(); 
        }
        else 
        {
            AudioManager.Instance.Play("Spike Collision");
            Destroy(gameObject);
        }
    }

    private void CheckIfPlayerIsInSight(Transform spikePosition)
    {
        var raycastHit = Physics2D.Raycast(spikePosition.position, raycastCheckDirection,
            maxDistance, layerMask);
        if (raycastHit.collider != null &&
            raycastHit.collider.GetComponent<PlayerMovementController>() != null)
        {
            rigidbody2D.simulated = true;
            AudioManager.Instance.Play("Attacking Spike");
            MoveSpike();
        }
    }

    private void MoveSpike()
    {
        rigidbody2D.AddForce(raycastCheckDirection * spikeSpeed);
        StartCoroutine("WaitToDestroy");
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
