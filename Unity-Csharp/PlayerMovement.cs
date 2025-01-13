using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 9f;
    public float jumpHeight = 3f;
    public float gravity = -19.62f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is grounded using a sphere overlap.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            // Reset the velocity when the player is grounded.
            velocity.y = -2f;
        }

        // Get input for horizontal and vertical movement.
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // Calculate the movement direction based on input and player orientation.
        Vector3 move = transform.right * x + transform.forward * z;

        // Move the player character.
        controller.Move(move.normalized * speed * Time.deltaTime);

        // Handle jumping if the player is grounded and presses the jump button.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity to the velocity.
        velocity.y += gravity * Time.deltaTime;

        // Move the player character vertically based on gravity and jumping.
        controller.Move(velocity * Time.deltaTime);
    }
}
