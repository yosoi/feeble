using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  const float GRAVITY = -9.81f;

  [Header("Configuration")]
  public float JumpHeight;
  public float Speed;

  [Header("Dependencies")]
  public CharacterController Controller;

  private Vector3 velocity;

  // unity method called every frame
  private void Update() {
    Move();
  }

  // move the character using player input
  private void Move() {
    // determine if player is on the ground
    bool isControllerGrounded = Controller.isGrounded;

    // stop moving down if grounded
    if (isControllerGrounded && velocity.y < 0) {
      velocity.y = 0;
    }

    // get player input
    Vector3 horizontalInput = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

    // apply lateral movement to the player controller
    Controller.Move(horizontalInput * Time.deltaTime * Speed);

    // // orient game object using input
    // if (horizontalInput != Vector3.zero) {
    //   gameObject.transform.forward = horizontalInput;
    // }

    // jump when button pressed, if grounded
    if (Input.GetButtonDown("Jump") && isControllerGrounded) {
      Debug.Log("jump");
      velocity.y += Mathf.Sqrt(JumpHeight * -3.0f * GRAVITY);
    }

    // apply gravity
    velocity.y += GRAVITY * Time.deltaTime;

    // update velocity to the player controller
    Controller.Move(velocity * Time.deltaTime);
  }
}
