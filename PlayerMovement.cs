using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Refrences")]
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask, enemyAreaMask;

    [Header("Player Options")]
    public float moveSpeed = 12f;
    public float sprintSpeed = 18f;
    public float jumpHeight = 2f;
    public float stepOffset = 0.7f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;

    float animTransitionSpeed = 15f;

    Vector3 velocity;

    public bool isGrounded;

    float speed;
    
    void Update()
    {
        GroundCheck();

        ToggleSpriting();

        Vector3 move = GetPlayerInput();
        controller.Move(move * speed * Time.deltaTime);
        
        Jump();
        Gravity();
    }

    private void ToggleSpriting()
    {
        speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
    }

    private void Gravity()
    {
        // Apply Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void GroundCheck()
    {
        // Check if Player is Grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(-2f * gravity * jumpHeight);
            controller.stepOffset = 0f;
        }

        if(velocity.y < 0 && isGrounded)
        {
            controller.stepOffset = stepOffset;
        }

    }

    public Vector3 GetPlayerInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized;
        return move;
    }

    public Vector3 EnemyReachPosition()
    {
        Vector3 playerPos = transform.position;
        if(Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit,enemyAreaMask))
        {
            playerPos = new Vector3(transform.position.x, hit.point.y - 5, transform.position.z);
        }

        return playerPos;
    }

    public Vector3 GetPlayerVelocity()
    {
        return velocity;
    }
}
