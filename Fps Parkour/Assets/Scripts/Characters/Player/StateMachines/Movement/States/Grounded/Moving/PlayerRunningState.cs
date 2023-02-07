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
        stateMachine.Player.InputManager.CrouchEvent += OnCrouchPressed;
        stateMachine.Player.InputManager.JumpEvent += OnJump;
        stateMachine.Player.InputManager.JumpEvent += OnVaultPressed;
    }
    public override void Exit()
    {
        base.Exit();
        stateMachine.Player.InputManager.MovingCanceled -= OnMoveCancelled;
        stateMachine.Player.InputManager.CrouchEvent -= OnCrouchPressed;
        stateMachine.Player.InputManager.JumpEvent -= OnJump;
        stateMachine.Player.InputManager.JumpEvent -= OnVaultPressed;
    }

    protected void OnMoveCancelled()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
    }
   
}
