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
        stateMachine.Player.InputManager.JumpEvent += OnVaultPressed;
        stateMachine.Player.InputManager.JumpEvent += OnJump;
    }
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        //stateMachine.Player.Animator.SetFloat("moveAmount", Mathf.Abs(movementInput.y));
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
