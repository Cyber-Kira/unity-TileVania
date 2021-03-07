using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5f;

    // State
    bool isAlive = true;

    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;

    private void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }
    private void Update() {
        Run();
        FlipSprite();
    }

    private void Run() {
        float controlThrow = Input.GetAxis("Horizontal") * runSpeed;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        Vector2 playerVelocity = new Vector2(controlThrow, myRigidBody.velocity.y);

        myRigidBody.velocity = playerVelocity;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void FlipSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
}
