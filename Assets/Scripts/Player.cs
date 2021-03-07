using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 1f;

    // State
    bool isAlive = true;

    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2D;
    float gravityScaleAtStart;

    private void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }
    private void Update() {
        Run();
        Jump();
        ClimbLadder();
        FlipSprite();
    }

    private void Run() {
        float controlThrow = Input.GetAxis("Horizontal") * runSpeed;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        Vector2 playerVelocity = new Vector2(controlThrow, myRigidBody.velocity.y);

        myRigidBody.velocity = playerVelocity;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Jump() {

        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            return;
        }
        if (Input.GetButtonDown("Jump")) {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void ClimbLadder() {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder"))) {
            myRigidBody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("Climbing", false);
            return;
        }

        float controlThrow = Input.GetAxis("Vertical") * climbSpeed;
        Vector2 climbingVelocityToAdd = new Vector2(myRigidBody.velocity.x, controlThrow);
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;

        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
        myRigidBody.velocity = climbingVelocityToAdd;
        myRigidBody.gravityScale = 0f;
    }

    private void FlipSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
}
