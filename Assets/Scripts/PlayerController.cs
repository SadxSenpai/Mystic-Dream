using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.InputSystem;

// Takes and handles input and movement for a player character
public class PlayerController : MonoBehaviour
{
    public PlayerAnimationStrings animationStrings;
    public bool CharacterPhysicsEnabled
    {
        get
        {
            return CharacterPhysicsEnabled;
        }
        set
        {
            if (value == true)
            {
                rb.simulated = true;
            }
            else
            {
                rb.velocity = Vector2.zero;
                rb.simulated = false;
            }
        }
    }

    public float moveSpeed = 5f;


    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Collider2D swordCollider;
    Vector2 moveInput;

    bool canMove = true;
    bool animLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // Control animation parameters
        if (!animLocked && moveInput != Vector2.zero)
        {
            animator.SetFloat(animationStrings.moveX, moveInput.x);
            animator.SetFloat(animationStrings.moveY, moveInput.y);
        }
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            if (canMove == true && moveInput != Vector2.zero)
            {
                // Move animation and add velocity
                // Accelerate the player while run direction is pressed(limited by rigidbody linear drag)
                rb.AddForce(moveInput * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Force);

                if (moveInput.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (moveInput.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
            }
        }
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if (!animLocked && canMove)
        {
            if (moveInput != Vector2.zero)
            {
                animator.Play(animationStrings.walk);
            }
            else
            {
                animator.Play(animationStrings.idle);

            }
        }
    }

    void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        if (canMove)
        {
            animator.Play(animationStrings.attack);
            canMove = false;
        }
    }
    // Allow animations to be freely selected and the character to move again
    void UnlockAnimation()
    {
        canMove = true;
        animLocked = false;
    }

    // Lock animation into hit animation
   /* public void OnHit()
    {
        animLocked = true;
        animator.Play(animationStrings.hit);

    }

    // Lock animation into death animation
    public void OnDeath()
    {
        canMove = false;
        animLocked = true;
        animator.Play(animationStrings.die);
    } */
}
