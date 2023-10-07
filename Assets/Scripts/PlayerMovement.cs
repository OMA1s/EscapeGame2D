using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Collider2D myBodyCollider;
    [SerializeField] float runSpeed = 5;
    [SerializeField] float jumpSpeed = 2;
    [SerializeField] float climbSpeed = 3;
    BoxCollider2D myFeetCollider;
    SpriteRenderer mySpriteRenderer;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] bool isAlive = true;

    float gravityScale;

    Animator myAnimator;
    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<Collider2D>();
        gravityScale = myRigidBody.gravityScale;
        myFeetCollider = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if (isAlive)
        {
            Run();
            flipSprite();
            climb();
        }
        die();
    }

    void die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("isDead");
            myRigidBody.velocity = new Vector2(0, 10f);
            mySpriteRenderer.color = Color.red;
            StartCoroutine(processPlayerdie());
        }
    }
    IEnumerator processPlayerdie()
    {
        yield return new WaitForSecondsRealtime(1);
        FindObjectOfType<GameSession>().processPlayerDeaths();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        if (value.isPressed)
        {
            GameObject b = Instantiate(bullet, gun.position, transform.rotation);
        }
        
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void climb()
    {
        bool playerHasVerticalMovespeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalMovespeed);
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRigidBody.gravityScale = gravityScale;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
        else
        {
            myRigidBody.gravityScale = 0;
            Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x, moveInput.y * climbSpeed);
            myRigidBody.velocity = playerVelocity;
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalMovespeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalMovespeed);
    }

    void flipSprite()
    {
        bool playerHasHorizontalMovespeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalMovespeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
}
