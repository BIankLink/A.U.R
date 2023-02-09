using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlidingState : PlayerGroundedState
{
    public PlayerSlidingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateMachine.Player.CapsuleCollider.height = stateMachine.Player.crouchHeight;
        
    }
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        stateMachine.Player.SlideTimer += deltaTime;
        if(stateMachine.Player.SlideTimer >= stateMachine.Player.maxSlideTime)
        {
            stateMachine.ChangeState(stateMachine.CrouchingState);
        }
    }
    public override void PhysicsUpdate(float deltaTime)
    {
        SlideForwards(deltaTime);

    }
    void SlideForwards(float deltaTime)
    {
        stateMachine.Player.ActSpeed = stateMachine.Player.SlideSpeedLimit;
        stateMachine.Player.AdjustmentAmt = 0;
        Vector3 Dir = stateMachine.Player.Rigidbody.velocity.normalized;
        Dir.y = 0;
        stateMachine.Player.Rigidbody.AddForce(Dir * stateMachine.Player.SlideAmt*deltaTime, ForceMode.Impulse);
        
    }
}
