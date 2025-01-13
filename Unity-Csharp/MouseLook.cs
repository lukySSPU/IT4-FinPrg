using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Locks the cursor to the center of the screen and makes it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse movement values for horizontal (X) and vertical (Y) axes
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Update the vertical rotation and clamp it to prevent over-rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply vertical rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player's body horizontally based on mouse movement
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
