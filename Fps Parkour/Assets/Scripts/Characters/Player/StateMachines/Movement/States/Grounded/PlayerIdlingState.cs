using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdlingState : PlayerGroundedState
{
    public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        stateMachine.Player.SlideTimer = 0f;
        stateMachine.Player.InputManager.JumpEvent += OnVaultPressed;
        stateMachine.Player.InputManager.JumpEvent += OnJump;
        stateMachine.Player.InputManager.CrouchEvent += OnCrouchPressed;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        //stateMachine.Player.Animator.SetFloat("moveAmount", Mathf.Abs(movementInput.y));
        if(movementInput == Vector2.zero)
        {
            return;
        }
        OnMoved();
    }
    public override void PhysicsUpdate(float deltaTime)
    {
        base.PhysicsUpdate(deltaTime);
        //stateMachine.Player.TargetSpeed = 0f;
    }
    public override void Exit()
    {
        base.Exit();
        stateMachine.Player.InputManager.CrouchEvent -= OnCrouchPressed;
        stateMachine.Player.InputManager.JumpEvent -= OnJump;
        stateMachine.Player.InputManager.JumpEvent -= OnVaultPressed;
    }

}
