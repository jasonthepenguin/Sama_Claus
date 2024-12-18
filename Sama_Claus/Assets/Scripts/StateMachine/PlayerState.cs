using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// base state for all player state 
public class PlayerState 
{
    // reference to the player here .. when you get the chance
    public Player player; // set in inspector
    protected PlayerStateMachine playerStateMachine;

    public PlayerState(Player player, PlayerStateMachine playerStateMachine)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
    }

    public virtual void EnterState() {

    }

    public virtual void ExitState() {

    }

    public virtual void FrameUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {

    }

}
