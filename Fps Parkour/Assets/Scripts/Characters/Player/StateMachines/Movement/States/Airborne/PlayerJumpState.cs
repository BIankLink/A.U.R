using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirborneState
{
    public PlayerJumpState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        JumpUp();
    }
    void JumpUp()
    {
        Vector3 vel = stateMachine.Player.Rigidbody.velocity;
        vel.y = 0;
        stateMachine.Player.Rigidbody.velocity = vel;

        stateMachine.Player.Rigidbody.AddForce(stateMachine.Player.transform.up * stateMachine.Player.JumpAmt, ForceMode.Impulse);
        InAir();
    }
}
