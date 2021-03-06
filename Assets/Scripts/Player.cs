using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    Rigidbody2D myRigidBody;

    private void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        Run();
    }

    private void Run() {
        float controlThrow = Input.GetAxis("Horizontal") * runSpeed;
        Vector2 playerVelocity = new Vector2(controlThrow, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    }
}
