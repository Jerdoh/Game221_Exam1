using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D player;
    private BoxCollider2D playerCollision;
    private SpriteRenderer playerSprite;
    private Animator playerAnimation;

    [SerializeField] private LayerMask jumpableGround;

    private float jumpValue = 14f;
    private float moveValue = 4f;

    private enum MovementState { idle, running, jumping, falling }
    
    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerCollision = GetComponent<BoxCollider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(dirX * moveValue, player.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            player.velocity = new Vector2(player.velocity.x, jumpValue);
        }

        UpdateAnimationState(dirX);
    }

    private void UpdateAnimationState(float direction)
    {
        MovementState state;

        if (direction > 0f)
        {
            state = MovementState.running;
            playerSprite.flipX = false;
        }
        else if (direction < 0f)
        {
            state = MovementState.running;
            playerSprite.flipX = true;
        }
        else 
        {
            state = MovementState.idle;
        }

        if (player.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (player.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        playerAnimation.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(playerCollision.bounds.center, playerCollision.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
