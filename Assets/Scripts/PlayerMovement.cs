using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float turnSpeed = 150f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Animator animator;
    private Vector3 verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (controller.isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2f;
        }

        // A/D rotates the player left/right
        float turnInput = Input.GetAxis("Horizontal");
        transform.Rotate(0f, turnInput * turnSpeed * Time.deltaTime, 0f);

        // W/S moves forwards/backwards relative to facing direction
        float forwardInput = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.forward * forwardInput;

        if (forwardInput != 0)
        {
            controller.Move(move * moveSpeed * Time.deltaTime);
        }

        // Gravity
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);

        // Send forward movement to Animator
        animator.SetFloat("Speed", Mathf.Abs(forwardInput));
    }
}