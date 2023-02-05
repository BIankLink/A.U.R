using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState : PlayerMovementState
{
    public PlayerAirborneState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        turnSpd = stateMachine.Player.TurnSpeedOnAir;
        control = stateMachine.Player.InAirControl;
        stateMachine.Player.GroundTimer = 0;
        //StartAnimation(stateMachine.Player.AnimationData.AirborneParameterHash);  
    }
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        stateMachine.Player.CheckG = stateMachine.Player.Collision.CheckFloor(-stateMachine.Player.transform.up);
        if (stateMachine.Player.CheckG)
        {
            OnContactWithGround();
        }
    }
    public override void PhysicsUpdate(float deltaTime)
    {
        base.PhysicsUpdate(deltaTime);
        if (stateMachine.Player.InAirTimer < 10f)
            stateMachine.Player.InAirTimer += deltaTime;
    }
    public override void Exit()
    {
        base.Exit();
        //StopAnimation(stateMachine.Player.AnimationData.AirborneParameterHash);
    }

    protected override void OnContactWithGround()
    {
        stateMachine.ChangeState(stateMachine.LightLandingState);
    }
}
