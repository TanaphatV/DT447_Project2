using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    public float jumpForce = 5.0f;

    private CharacterController controller;
    private Camera playerCamera;
    private Vector3 moveDirection = Vector3.zero;
    private float verticalRotation = 0;
    private bool isGrounded;

    public bool talking;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(!talking)
            Movement();
    }

    void Movement()
    {
        // Player Movement
        float forwardSpeed = Input.GetAxis("Vertical") * speed;
        float sideSpeed = Input.GetAxis("Horizontal") * speed;

        moveDirection = new Vector3(sideSpeed, 0, forwardSpeed);
        moveDirection = transform.TransformDirection(moveDirection);

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
        }

        // Apply gravity
        if (!controller.isGrounded)
        {
            moveDirection.y -= 9.81f * Time.deltaTime;
        }

        // Player Look
        float rotLeftRight = Input.GetAxis("Mouse X") * sensitivity;
        transform.Rotate(0, rotLeftRight, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * sensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        isGrounded = hit.normal.y > 0.9;
    }
}
