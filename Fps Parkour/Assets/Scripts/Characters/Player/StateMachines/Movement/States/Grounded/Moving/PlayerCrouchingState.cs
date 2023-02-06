using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchingState : PlayerGroundedState
{
    public PlayerCrouchingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateMachine.Player.CapsuleCollider.height = stateMachine.Player.crouchHeight;
        
        stateMachine.Player.InputManager.CrouchEvent += OnCrouchCancel;
        if(stateMachine.Player.ActSpeed> stateMachine.Player.SlideSpeedLimit)
        {
             stateMachine.ChangeState(stateMachine.SlidingState);
        }
    }
    public override void PhysicsUpdate(float deltaTime)
    {
        base.PhysicsUpdate(deltaTime);
        stateMachine.Player.TargetSpeed = stateMachine.Player.crouchSpeed;
    }
    public override void Exit()
    {
        base.Exit();
        stateMachine.Player.InputManager.CrouchEvent -= OnCrouchCancel;
        stateMachine.Player.CapsuleCollider.height = stateMachine.Player.StandingHeight;
    }
    void OnCrouchCancel()
    {
        bool Check = stateMachine.Player.Collision.CheckRoof(stateMachine.Player.transform.up);
        if (!Check)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }
}
