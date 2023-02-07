using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerAirborneState
{
    private Vector3 playerPositionOnEnter;
    public PlayerInAirState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();

        
        // StartAnimation(stateMachine.Player.AnimationData.FallParameterHash);

        

        playerPositionOnEnter = stateMachine.Player.transform.position;

        ResetVerticalVelocity();
    }
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        stateMachine.Player.CheckW = CheckWall(movementInput.x, movementInput.y);
        if (stateMachine.Player.CheckW)
        {
            OnWallRun();
            return;
        }
        stateMachine.Player.CheckG = stateMachine.Player.Collision.CheckFloor(-stateMachine.Player.transform.up);
        if (stateMachine.Player.CheckG && stateMachine.Player.InAirTimer > 0.2f)
        {
            OnContactWithGround();
            return;
        }
    }
    public override void PhysicsUpdate(float deltaTime)
    {
        base.PhysicsUpdate(deltaTime);
        if (stateMachine.Player.InAirTimer < 10f)
            stateMachine.Player.InAirTimer += deltaTime;
    }
    protected override void OnContactWithGround()
    {
        float fallDistance = playerPositionOnEnter.y - stateMachine.Player.transform.position.y;

        if (fallDistance <  stateMachine.Player.MinimumDistanceToBeConsideredHardFall)
        {
            stateMachine.ChangeState(stateMachine.LightLandingState);

            return;
        }

        stateMachine.ChangeState(stateMachine.HardLandingState);

    }
}
