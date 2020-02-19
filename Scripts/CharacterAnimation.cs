using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{

    private Animator animator; 
    private IMove mover; 
    private SpriteRenderer spriteRenderer; 

    private void Awake() {
        animator = GetComponent<Animator>(); 
        mover = GetComponent<IMove>(); 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        float speed = mover.Speed; 
        animator.SetFloat("Speed", Mathf.Abs(speed)); 

        // Because speed > 0 gives you a bool and flipX expects a bool this works
        // Only flips if you're moving in one direction or another
        if (speed != 0) {
            spriteRenderer.flipX = speed > 0; 
        }
    }
}
