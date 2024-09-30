using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public bool lockCursor;

    public float mouseSensitivity = 5;

    public Transform target;
    public float distFromTarget = 20;

    public Vector2 pitchMinMax = new Vector2(-10, 85);

    public float rorationSmoothTime = 0.3f;
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;
    
    private float pitch; // rotation x axes
    private float yaw; // y axes

    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void LateUpdate() // is called after others Update methods
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation,  new Vector3(pitch, yaw), ref rotationSmoothVelocity, rorationSmoothTime);
        transform.eulerAngles = currentRotation;
        
        transform.position = target.position - transform.forward * distFromTarget; 
    }
}
