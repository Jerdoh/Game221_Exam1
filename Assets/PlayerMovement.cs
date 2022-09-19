using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D player;
    private SpriteRenderer playerSprite;
    private Animator playerAnimation;
    private float jumpValue = 14f;
    private float moveValue = 4f;

    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(dirX * moveValue, player.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            player.velocity = new Vector2(player.velocity.x, jumpValue);
        }

        UpdateAnimationState(dirX);
    }

    private void UpdateAnimationState(float direction)
    {
        if (direction > 0f)
        {
            playerAnimation.SetBool("running", true);
            playerSprite.flipX = false;
        }
        else if (direction < 0f)
        {
            playerAnimation.SetBool("running", true);
            playerSprite.flipX = true;
        }
        else 
        {
            playerAnimation.SetBool("running", false);
        }
    }
}
