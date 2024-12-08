using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Assign this in the Inspector by dragging in your Player GameObject
    public Transform player;

    // Adjust this speed in the Inspector if desired
    public float moveSpeed = 2f;

    private SpriteRenderer spriteRenderer;
    private bool isAngry = false;
    private Sprite angrySprite;

    void Start()
    {
        // Get the SpriteRenderer on this NPC
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Load the "Sama_angry" sprite from Assets/Resources/Textures folder
        // Make sure you have: Assets/Resources/Textures/Sama_angry.png
        angrySprite = Resources.Load<Sprite>("Textures/Sama_angry");

        // Start the coroutine to change sprite after 5 seconds
        StartCoroutine(BecomeAngry());
    }

    IEnumerator BecomeAngry()
    {
        yield return new WaitForSeconds(5f);

        // Change the sprite to Sama_angry
        if (angrySprite != null)
        {
            spriteRenderer.sprite = angrySprite;
            isAngry = true; // Enable movement towards player
        }
        else
        {
            Debug.LogWarning("Could not load Sama_angry sprite. Check the path and file name.");
        }
    }

    void Update()
    {
        if (player == null)
            return;

        // Calculate the direction from this NPC to the player
        Vector3 direction = player.position - transform.position;
        // Prevent tilting up/down
        direction.y = 0;

        // Rotate to face the player
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = targetRotation;
        }

        // If angry, move towards the player
        if (isAngry)
        {
            // Move NPC closer to the player at moveSpeed
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
}