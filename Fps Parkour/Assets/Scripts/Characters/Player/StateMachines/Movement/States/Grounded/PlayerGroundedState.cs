using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerMovementState
{
    protected ObstacleHitData hitData;
    protected ParkourAction action;
    public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        turnSpd = stateMachine.Player.TurnSpeed;
        stateMachine.Player.InputManager.JumpEvent += OnVaultPressed;
        stateMachine.Player.InputManager.JumpEvent += OnJump;
        control = 1;
        stateMachine.Player.InAirTimer = 0;
    }
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        hitData = stateMachine.Player.EnvironmentScanner.ObstacleCheck();
       
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
    public override void Exit()
    {
        base.Exit();
        stateMachine.Player.InputManager.JumpEvent -= OnVaultPressed;
        stateMachine.Player.InputManager.JumpEvent -= OnJump;
    }
    protected void OnMoved()
    {
        stateMachine.ChangeState(stateMachine.RunningState);
    }
    protected void OnJump()
    {
        stateMachine.ChangeState(stateMachine.JumpState);
    }
    void OnVaultPressed()
    {
        if (hitData.forwardHitFound)
        {
            foreach (ParkourAction action in stateMachine.Player.ParkourActions)
            {
                if (action.CheckIfPossible(hitData, stateMachine.Player.transform))
                {
                    this.action = action;
                    stateMachine.ChangeState(stateMachine.VaultingState);
                }
            }
        }
    }
}
