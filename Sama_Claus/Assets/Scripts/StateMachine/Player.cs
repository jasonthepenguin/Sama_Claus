using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public float climbSpeed = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool nearLadder;

    public float interactDistance = 3f;



    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isMoving;
    [HideInInspector] public Vector3 lastPosition;

    public PlayerStateMachine StateMachine { get; set;}
    public PlayerWalkingState WalkState {get; set;}

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        lastPosition = transform.position;

        StateMachine = new PlayerStateMachine();

        WalkState = new PlayerWalkingState(this, StateMachine);
    }


    // Start is called before the first frame update
    private void Start()
    {
        nearLadder = false;

        StateMachine.Initialize(WalkState);
        
    }

    private void Update()
    {

        // state machine update 
        StateMachine.CurrentPlayerState.FrameUpdate();

        // interaction check
        if(Input.GetKeyDown(KeyCode.E))
        {
            // Raycast from center of screen (cam forward)
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {

                //print(hit);
                // Check if object has I_Interactable interface
                I_Interactable interactable = hit.collider.GetComponent<I_Interactable>();
                if(interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentPlayerState.PhysicsUpdate();
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        // TODO: fill in once statemachine is created

        StateMachine.CurrentPlayerState.AnimationTriggerEvent(triggerType);
    }
    public enum AnimationTriggerType{
        HandToFace,
        TorchDown

    }


}
