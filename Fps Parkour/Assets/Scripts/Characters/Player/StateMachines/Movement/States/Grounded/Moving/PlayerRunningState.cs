using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerGroundedState
{
    public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateMachine.Player.InputManager.MovingCanceled += OnMoveCancelled;
        stateMachine.Player.InputManager.JumpEvent += OnJump;
    }
    public override void Exit()
    {
        base.Exit();
        stateMachine.Player.InputManager.MovingCanceled -= OnMoveCancelled;
        stateMachine.Player.InputManager.JumpEvent -= OnJump;
    }

    protected void OnMoveCancelled()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }
}
