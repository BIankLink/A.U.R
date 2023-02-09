using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerGroundedState
{
    public PlayerLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.ChangeState(stateMachine.IdlingState);
        //Start AnimationHash
    }
    public override void Exit()
    {
        base.Exit();
        //stop animationhash
    }
}
