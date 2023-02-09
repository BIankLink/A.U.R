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
        SetSpeedToVelocity();
        stateMachine.Player.Animator.applyRootMotion = false;
        SetOnGround();
    }

    protected void SetOnGround()
    {
        turnSpd = stateMachine.Player.TurnSpeed;
        //stateMachine.Player.InputManager.JumpEvent += OnVaultPressed;
        //stateMachine.Player.InputManager.JumpEvent += OnJump;
        control = 1;
        stateMachine.Player.InAirTimer = 0;
        stateMachine.Player.ActWallRunTime = 0;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        //stateMachine.Player.hitData = stateMachine.Player.EnvironmentScanner.ObstacleCheck();
       
        stateMachine.Player.CheckG = stateMachine.Player.Collision.CheckFloor(-stateMachine.Player.transform.up);
        if (!stateMachine.Player.CheckG)
        {
            //InAir
            stateMachine.ChangeState(stateMachine.InAirState);
        }
    }
    public override void PhysicsUpdate(float deltaTime)
    {
        stateMachine.Player.TargetSpeed = Mathf.Lerp(stateMachine.Player.BackwardsMovementSpeed, stateMachine.Player.MaxSpeed, movementInput.y);
        if (stateMachine.Player.AdjustmentAmt < 1)
        {
            stateMachine.Player.AdjustmentAmt += deltaTime * stateMachine.Player.PlayerCtrl;
        }
        else
        {
            stateMachine.Player.AdjustmentAmt = 1;
        }
        LerpSpeed(inputMag, deltaTime, stateMachine.Player.TargetSpeed);
        base.PhysicsUpdate(deltaTime);
        if (stateMachine.Player.GroundTimer < 10)
        {
            stateMachine.Player.GroundTimer += deltaTime;
        }
    }
    public override void Exit()
    {
        base.Exit();
        //stateMachine.Player.InputManager.JumpEvent -= OnVaultPressed;
        //stateMachine.Player.InputManager.JumpEvent -= OnJump;
    }
    protected void OnMoved()
    {
        stateMachine.ChangeState(stateMachine.RunningState);
    }
    protected void OnJump()
    {
        if (ledgePos != Vector3.zero)
        {
            return;
        }
            stateMachine.ChangeState(stateMachine.JumpState);
    }
   
    protected void OnCrouchPressed()
    {
        stateMachine.ChangeState(stateMachine.CrouchingState);
        if (stateMachine.Player.ActSpeed > stateMachine.Player.SlideSpeedLimit)
        {
            stateMachine.ChangeState(stateMachine.SlidingState);
        }
    }
    
}
