using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class LookToCamera : MonoBehaviour
{
    // Reference to the main camera transform component
    private Transform MainCameraTransform;

    private void Awake()
    {
        // Get the reference to the main camera transform component
        MainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        // Rotate the game object to face the main camera
        transform.rotation = Quaternion.LookRotation(-MainCameraTransform.position + transform.position);

        // Draw a debug line from the game object to the main camera to visualize the facing direction
        Debug.DrawLine(transform.position, MainCameraTransform.position, Color.red);
    }
}
