using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        // Check initial transform orientation
        Debug.Log("Initial transform.forward: " + transform.forward);
    }

    void Update()
    {
        // Grounded check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
       // Debug.Log("IsGrounded: " + isGrounded);

        // If grounded and velocity is downwards, reset it
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
       // Debug.Log("Input X: " + x + " | Input Z: " + z);

        // Debug the orientation vectors
      //  Debug.Log("Transform forward: " + transform.forward);
      //  Debug.Log("Transform right: " + transform.right);

        // Compute movement vector
        Vector3 move = transform.right * x + transform.forward * z;
       // Debug.Log("Move vector: " + move);

        // Move the character horizontally
        controller.Move(move * speed * Time.deltaTime);

        // Jump if possible
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
           // Debug.Log("Jump triggered. velocity.y set to: " + velocity.y);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        //Debug.Log("Velocity after gravity: " + velocity);

        // Move character according to velocity (vertical motion)
        controller.Move(velocity * Time.deltaTime);

        // Check movement state
        if (lastPosition != transform.position && isGrounded)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

       // Debug.Log("Current Position: " + transform.position + " | Last Position: " + lastPosition + " | isMoving: " + isMoving);

        // Update last position
        lastPosition = transform.position;
    }
}