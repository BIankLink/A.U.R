using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;
    protected Vector2 movementInput;
    protected float control;
    protected float turnSpd;
    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
    {
        stateMachine = playerMovementStateMachine;
    }
    #region IState
    public virtual void Enter()
    {
        //Debug.Log("State: " +GetType().Name);
    }
    public virtual void HandleInput()
    {
        ReadMovementInput();
    }
    public virtual void Update(float deltaTime)
    {
        
    }
    public virtual void PhysicsUpdate(float deltaTime)
    {
        Move(deltaTime);
    }
    public virtual void Exit()
    {
        
    }
    public virtual void OnAnimationEnterEvent()
    {
        
    }

    public virtual void OnAnimationExitEvent()
    {
        
    }

    public virtual void OnAnimationTransitionEvent()
    {
        
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        
    }

    public virtual void OnTriggerExit(Collider collider)
    {
        
    }
    #endregion


    #region Main Methods
    private void ReadMovementInput()
    {
        movementInput = stateMachine.Player.InputManager.move;
    }
    private void Move(float deltaTime)
    {
        
        //Vector3 movementDirection = GetMovementInputDirection();
        float inputMag = new Vector2(movementInput.x, movementInput.y).normalized.magnitude;
        stateMachine.Player.TargetSpeed = Mathf.Lerp(stateMachine.Player.BackwardsMovementSpeed, stateMachine.Player.MaxSpeed, movementInput.y);

        LerpSpeed(inputMag, deltaTime, stateMachine.Player.TargetSpeed);

        MovePlayer(movementInput.x, movementInput.y, deltaTime,control);
        TurnPlayer(stateMachine.Player.InputManager.look.x, deltaTime, turnSpd);

        if (stateMachine.Player.AdjustmentAmt < 1)
        {
            stateMachine.Player.AdjustmentAmt += deltaTime * stateMachine.Player.PlayerCtrl;
        }
        else
        {
            stateMachine.Player.AdjustmentAmt = 1;
        }
    }
    protected virtual void OnContactWithGround()
    {
    }

    #endregion

    #region Reusable Methods
    protected Vector3 GetMovementInputDirection()
    {
        return new Vector3(movementInput.x, 0f, movementInput.y);
    }
    void LerpSpeed(float magnitude,float d,float spd)
    {
        float LaMT = spd * magnitude;
        float Accel = stateMachine.Player.Acceleration;
        if (magnitude == 0)
        {
            Accel = stateMachine.Player.Decceleration;
        }

        stateMachine.Player.ActSpeed = Mathf.Lerp(stateMachine.Player.ActSpeed, LaMT, d * Accel);
    }

    protected void TurnPlayer(float xAmt, float deltaTime, float turnSpeed)
    {
        stateMachine.Player.Yturn += (xAmt * deltaTime)*turnSpeed;
        stateMachine.Player.transform.rotation = Quaternion.Euler(0, stateMachine.Player.Yturn, 0);
    }

    protected void MovePlayer(float hor, float ver, float deltaTime,float control)
    {
        Vector3 MoveDir = (stateMachine.Player.transform.forward * ver) + (stateMachine.Player.transform.right * hor);
        MoveDir = MoveDir.normalized;

        //if we are not pressing input,carryon in the direction of velocity
        if(hor==0 && ver == 0)
        {
            MoveDir = stateMachine.Player.Rigidbody.velocity.normalized;
        }

        MoveDir = MoveDir * stateMachine.Player.ActSpeed;

        MoveDir.y = stateMachine.Player.Rigidbody.velocity.y;

        //apply Acceleration
        float acel = (stateMachine.Player.DirectionalControl * stateMachine.Player.AdjustmentAmt)* control;//how much control we have over our movement
        Vector3 lerpVel = Vector3.Lerp(stateMachine.Player.Rigidbody.velocity, MoveDir, acel * deltaTime);
        stateMachine.Player.Rigidbody.velocity = lerpVel;
    }
    protected void ResetVelocity()
    {
        stateMachine.Player.Rigidbody.velocity = Vector3.zero;
    }
    protected Vector3 GetPlayerHorizontalVelocity()
    {
        Vector3 playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;

        playerHorizontalVelocity.y = 0f;

        return playerHorizontalVelocity;
    }
    protected bool IsMovingHorizontally(float minimumMagnitude = 0.1f)
    {
        Vector3 playerHorizontaVelocity = GetPlayerHorizontalVelocity();

        Vector2 playerHorizontalMovement = new Vector2(playerHorizontaVelocity.x, playerHorizontaVelocity.z);

        return playerHorizontalMovement.magnitude > minimumMagnitude;
    }
    protected Vector3 GetPlayerVerticalVelocity()
    {
        return new Vector3(0f, stateMachine.Player.Rigidbody.velocity.y, 0f);
    }
    protected void ResetVerticalVelocity()
    {
        Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

        stateMachine.Player.Rigidbody.velocity = playerHorizontalVelocity;
    }
    #endregion
}
