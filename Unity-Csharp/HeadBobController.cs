using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    public bool enableHeadBob;

    [SerializeField, Range(0, 0.1f)] private float amp = 0.015f;
    [SerializeField, Range(0, 30)] private float frequency = 10f;

    [SerializeField] private Transform cam = null;
    [SerializeField] private Transform cameraHolder = null;

    public float toggleSpeed = 3f;
    private Vector3 startPos;
    private CharacterController controller;

    private Vector3 previousPosition;

    void Start()
    {
        // Initialize references and store the starting position of the camera
        controller = GetComponent<CharacterController>();
        startPos = cam.localPosition;
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enableHeadBob) return; // Exit early if head bobbing is disabled

        CheckMotion(); // Check if the player is moving and apply head bobbing if needed
        ResetPosition(); // Smoothly reset the camera position when the player stops moving
        //cam.LookAt(FocusTarget()); // Optional: Makes the camera look at a calculated focus target
    }

    // Applies a motion offset to the camera's local position
    void PlayMotion(Vector3 motion)
    {
        cam.localPosition += motion;
    }

    // Calculates the vertical and horizontal bobbing motion based on sine and cosine waves
    Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amp; // Vertical bobbing
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amp * 2; // Horizontal swaying
        return pos;
    }

    // Checks the player's motion and applies head bobbing if moving and grounded
    void CheckMotion()
    {
        float speed;

        // Calculate the player's movement vector
        Vector3 currentPosition = transform.position;
        Vector3 movement = currentPosition - previousPosition;
        speed = new Vector3(movement.x, 0, movement.z).magnitude / Time.deltaTime;

        previousPosition = currentPosition;

        // Only apply head bobbing if the player is moving fast enough and is grounded
        if (speed < toggleSpeed) return;
        if (!controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }

    // Smoothly resets the camera to its starting position when no motion is applied
    void ResetPosition()
    {
        if (cam.localPosition == startPos) return;
        cam.localPosition = Vector3.Lerp(cam.localPosition, startPos, 1 * Time.deltaTime);
    }

    // Calculates a point in front of the player for the camera to focus on
    Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(
            transform.position.x,
            transform.position.y + cameraHolder.localPosition.y,
            transform.localPosition.z
        );
        pos += cameraHolder.forward * 15f; // Forward offset for the focus point
        return pos;
    }
}
