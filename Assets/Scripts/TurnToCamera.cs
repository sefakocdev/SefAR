using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnToCamera : MonoBehaviour
{
    private Camera mainCamera;
    private bool turnToCameraEnabled = true;  // By default, turn to camera is enabled
    private Quaternion savedRotation;         // To store the rotation when turning to camera is disabled

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // If turning to camera is enabled
        if (turnToCameraEnabled && mainCamera != null)
        {
            // Make the object face the camera
            Vector3 lookAtPosition = mainCamera.transform.position;
            lookAtPosition.y = transform.position.y;  // Keep the y-axis fixed
            transform.LookAt(lookAtPosition);  // Rotate towards the camera

            // Correct rotation so it aligns as desired
            transform.rotation = Quaternion.LookRotation(transform.position - lookAtPosition, Vector3.up);
        }

        // Check for touch input
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            // Raycast to check if the object was tapped
            if (Physics.Raycast(ray, out hit))
            {
                // If the tapped object is this GameObject
                if (hit.collider.gameObject == gameObject)
                {
                    ToggleTurnToCamera();  // Toggle the turn to camera behavior
                }
            }
        }
    }

    private void ToggleTurnToCamera()
    {
        // If currently enabled, disable it and save the rotation
        if (turnToCameraEnabled)
        {
            savedRotation = transform.rotation;  // Save current rotation
            turnToCameraEnabled = false;         // Disable turning to camera
        }
        else
        {
            // Re-enable turning to camera
            turnToCameraEnabled = true;
        }
    }
}
