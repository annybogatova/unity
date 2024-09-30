using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public float walkSpeed = 4;
    public float runSpeed = 10;

    public float turnSmoothTime = 0.2f;
    private float turnSmoothVelocity;
    
    public float speedSmoothTime = 0.1f;
    private float speedSmoothVelocity;
    private float currentSpeed;

    private Animator animator;

    private Transform cameraTransform;
    void Start()
    {
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = input.normalized;

        if (inputDirection != Vector2.zero)
        {

            float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation,
                ref turnSmoothVelocity, turnSmoothTime);

            // not smooth
            //transform.eulerAngles = Vector3.up * Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg; // Atan in case y == 0
        }

        bool running = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float targetSpeed = (running ? runSpeed : walkSpeed) * inputDirection.magnitude; // stay still if nothing is pressed
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime); //smoothing speed too
        
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        float animationSpeedPercent = (running ? 1 : .5f) * inputDirection.magnitude;

        animator.SetFloat("SpeedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
    }
}
