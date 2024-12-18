using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingState : PlayerState
{
    public PlayerWalkingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {

    }

    public override void EnterState()
    {
        // Any initialization when we enter walking state.
        // For example, ensure gravity is enabled or reset vertical velocity if needed.
    }

    public override void ExitState()
    {
        // Cleanup if needed.
    }

    public override void FrameUpdate()
    {
        // Check if the player is grounded
        player.isGrounded = Physics.CheckSphere(player.groundCheck.position, player.groundDistance, player.groundMask);
        if (player.isGrounded && player.velocity.y < 0)
        {
            player.velocity.y = -2f;
        }

        // Get input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Compute move direction based on input
        Vector3 move = player.transform.right * x + player.transform.forward * z;

        // Store horizontal movement for PhysicsUpdate
        // Here we might store some temporary variables or directly apply them in PhysicsUpdate
        player.velocity.x = move.x * player.speed;
        player.velocity.z = move.z * player.speed;

        // Jump if possible
        if (Input.GetButtonDown("Jump") && player.isGrounded)
        {
            player.velocity.y = Mathf.Sqrt(player.jumpHeight * -2f * player.gravity);
        }

        // Update isMoving flag
        if (player.lastPosition != player.transform.position && player.isGrounded)
        {
            player.isMoving = true;
        }
        else
        {
            player.isMoving = false;
        }

        player.lastPosition = player.transform.position;

        // Consider transitions:
        // If you detect conditions to switch state (e.g. player near ladder), call stateMachine.ChangeState(new PlayerClimbingState(...))
    }

    public override void PhysicsUpdate()
    {
        // Simple test logic for climbing:
        // If player is near ladder and presses W or S, move vertically along the ladder instead of applying normal gravity.
        if (player.nearLadder)
        {
            float verticalInput = 0f;
            if (Input.GetKey(KeyCode.W))
            {
                verticalInput = 1f; // move up
            }
            else if (Input.GetKey(KeyCode.S))
            {
                verticalInput = -1f; // move down
            }

            if (verticalInput != 0f)
            {
                // Climbing: Override vertical velocity to move up/down the ladder.
                // No gravity applies in this scenario.
                //player.velocity.y = verticalInput * player.speed;
                player.velocity.y = verticalInput * player.climbSpeed;
            }
            else
            {
                // Near ladder but not climbing: apply normal gravity
                player.velocity.y += player.gravity * Time.deltaTime;
            }
        }
        else
        {
            // Normal gravity when not near a ladder
            player.velocity.y += player.gravity * Time.deltaTime;
        }

        // Apply final movement
        Vector3 horizontalMove = player.transform.right * (Input.GetAxis("Horizontal") * player.speed)
                                 + player.transform.forward * (Input.GetAxis("Vertical") * player.speed);

        player.controller.Move((horizontalMove * Time.deltaTime) + (Vector3.up * player.velocity.y * Time.deltaTime));
        



        /*
        // Apply gravity
        player.velocity.y += player.gravity * Time.deltaTime;

        // Apply movement via CharacterController
        // Note: player.velocity.x and z stored direction * speed, but we need to move in 3D vector form
        Vector3 moveVector = new Vector3(player.velocity.x, player.velocity.y, player.velocity.z);

        // Since we're using the CharacterController in the original code, we might want to separate horizontal and vertical movement:
        // The original code did something like:
        // controller.Move(move * speed * Time.deltaTime);
        // and then again controller.Move(velocity * Time.deltaTime) for vertical.
        // We can combine them here:
        Vector3 horizontalMove = player.transform.right * (Input.GetAxis("Horizontal") * player.speed) 
                                 + player.transform.forward * (Input.GetAxis("Vertical") * player.speed);
        player.controller.Move((horizontalMove * Time.deltaTime) + (Vector3.up * player.velocity.y * Time.deltaTime));
        */

    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        // Handle animation events if you have them
    }
}
