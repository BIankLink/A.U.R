using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRunState : PlayerAirborneState
{
    public PlayerWallRunState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        stateMachine.Player.CheckW = CheckWall(movementInput.x, movementInput.y);
        if (!stateMachine.Player.CheckW)
        {
            InAir();
            return;
        }
        stateMachine.Player.CheckG = stateMachine.Player.Collision.CheckFloor(-stateMachine.Player.transform.up);
        if (stateMachine.Player.CheckG)
        {
            OnContactWithGround();
            return;
        }
    }
    public override void PhysicsUpdate(float deltaTime)
    {
        
        stateMachine.Player.ActWallRunTime += deltaTime;
        //Debug.Log(ActWallRunTime);
        //turn our player with the in air modifier
        TurnPlayer(stateMachine.Player.InputManager.look.x, deltaTime, stateMachine.Player.TurnSpeedOnWalls);

        //move our player when on a wall
        WallMove(movementInput.y, deltaTime);
    }
    protected void WallMove(float verInput,float deltaTime)
    {
        //get direction to move in
        Vector3 moveDir = stateMachine.Player.transform.up * verInput;
        moveDir = moveDir * stateMachine.Player.WallRunUpwardsMovement;

        //carry forward our speed amount and momentum to the walls
        moveDir += stateMachine.Player.transform.forward * stateMachine.Player.ActSpeed;

        Vector3 lerpAmt = Vector3.Lerp(stateMachine.Player.Rigidbody.velocity, moveDir, stateMachine.Player.WallRunSpeedAcceleration * deltaTime);
        stateMachine.Player.Rigidbody.velocity = lerpAmt;
    }
}
