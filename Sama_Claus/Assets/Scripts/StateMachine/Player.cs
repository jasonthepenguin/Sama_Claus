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
        StateMachine.CurrentPlayerState.FrameUpdate();
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
