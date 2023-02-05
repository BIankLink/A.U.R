using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerMovementState
{
    public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        turnSpd = stateMachine.Player.TurnSpeed;
        control = 1;
        stateMachine.Player.InAirTimer = 0;
    }
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        stateMachine.Player.CheckG = stateMachine.Player.Collision.CheckFloor(-stateMachine.Player.transform.up);
        if (!stateMachine.Player.CheckG)
        {
            //InAir
            //stateMachine.ChangeState()
        }
    }
    public override void PhysicsUpdate(float deltaTime)
    {
        base.PhysicsUpdate(deltaTime);
        if (stateMachine.Player.GroundTimer < 10)
        {
            stateMachine.Player.GroundTimer += deltaTime;
        }
    }
    protected void OnMoved()
    {
        stateMachine.ChangeState(stateMachine.RunningState);
    }
    protected void OnJump()
    {
        stateMachine.ChangeState(stateMachine.JumpState);
    }
}
