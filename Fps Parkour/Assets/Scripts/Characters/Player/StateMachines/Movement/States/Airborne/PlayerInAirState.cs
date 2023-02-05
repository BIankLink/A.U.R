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

        stateMachine.Player.TargetSpeed = 0;

        playerPositionOnEnter = stateMachine.Player.transform.position;

        ResetVerticalVelocity();
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
