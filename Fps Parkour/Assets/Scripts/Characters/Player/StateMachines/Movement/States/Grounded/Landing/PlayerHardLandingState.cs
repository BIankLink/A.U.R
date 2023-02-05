using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHardLandingState : PlayerLandingState
{
    public PlayerHardLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Player.TargetSpeed = 0f;
        base.Enter();
        //StartAnimation(stateMachine.Player.AnimationData.HardLandParameterHash);
        stateMachine.Player.InputManager.input.actions.Disable();
        ResetVelocity();
    }
    public override void PhysicsUpdate(float deltaTime)
    {
        base.PhysicsUpdate(deltaTime);

        if (!IsMovingHorizontally())
        {
            return;
        }

        ResetVelocity();
    }
    public override void Exit()
    {
        base.Exit();
        //StopAnimation(stateMachine.Player.AnimationData.HardLandParameterHash);
        stateMachine.Player.InputManager.input.actions.Enable();
    }
    public override void OnAnimationExitEvent()
    {
        stateMachine.Player.InputManager.input.actions.Enable();
    }

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }
}
