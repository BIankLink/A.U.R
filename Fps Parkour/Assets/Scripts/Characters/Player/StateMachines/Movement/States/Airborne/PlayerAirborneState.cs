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
        
    }
    public override void PhysicsUpdate(float deltaTime)
    {
        base.PhysicsUpdate(deltaTime);
        
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
    protected void OnWallRun()
    {
        stateMachine.ChangeState(stateMachine.WallRunState);
    }
    protected bool CheckWall(float XM,float YM)
    {
        if (XM == 0 && YM == 0)
        {
            return false;
        }

        if (stateMachine.Player.ActWallRunTime > stateMachine.Player.WallRunTime)
        {
            return false;
        }

        Vector3 wallDirection = stateMachine.Player.transform.forward * YM + stateMachine.Player.transform.right * XM;
        wallDirection = wallDirection.normalized;

        bool wallCol = stateMachine.Player.Collision.CheckWalls(wallDirection);
        //Debug.Log(wallCol);
        return wallCol;
    }
    protected void InAir()
    {
        stateMachine.ChangeState(stateMachine.InAirState);
    }
}
