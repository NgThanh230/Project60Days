using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    //references
    public Animator animator;
    PlayerMovement playerMovement;
    SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.movement.x != 0 || playerMovement.movement.y != 0)    
        {
            animator.SetBool("Move", true);
            FlipSprite();
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }
    void FlipSprite(){
        if(playerMovement.lastHorizontalVector < 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
    
}
