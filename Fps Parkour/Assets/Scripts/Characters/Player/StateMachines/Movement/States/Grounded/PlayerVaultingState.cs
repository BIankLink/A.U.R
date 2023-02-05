using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVaultingState : PlayerGroundedState
{
    public PlayerVaultingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateMachine.Player.StartCoroutine(Vaulting(action));
    }

    protected IEnumerator Vaulting(ParkourAction action)
    {
        stateMachine.Player.InAction = true;
        stateMachine.Player.Rigidbody.useGravity = false;
        stateMachine.Player.InputManager.input.actions.Disable();
        stateMachine.Player.Animator.CrossFade(action.AnimationName, 0.2f);
        yield return null;
        var animState = stateMachine.Player.Animator.GetNextAnimatorStateInfo(0);
        if (!animState.IsName(action.AnimationName))
        {
            Debug.LogError("The Parkour Animation is wrong");
        }
        
        float timer = 0f;
        while (timer <= animState.length)
        {
            timer += Time.deltaTime;
            if (action.RotateToObstacle)
            {
                stateMachine.Player.transform.rotation= Quaternion.RotateTowards(stateMachine.Player.transform.rotation, action.TargetRotation, stateMachine.Player.TurnSpeed);
            }
            if (action.EnableTargetMatching)
            {
                MatchTarget(action);
            }

            if (stateMachine.Player.Animator.IsInTransition(0) && timer > 0.5f)
                break;

            yield return null;
        }
        yield return new WaitForSeconds(action.PostActionDelay);
        stateMachine.Player.InputManager.input.actions.Enable();
        stateMachine.Player.Rigidbody.useGravity=true;
        stateMachine.Player.InAction = false;
        stateMachine.ChangeState(stateMachine.IdlingState);
    }
    protected void MatchTarget(ParkourAction action)
    {
        if (stateMachine.Player.Animator.isMatchingTarget) { return; }
        stateMachine.Player.Animator.MatchTarget(action.MatchPosition, stateMachine.Player.transform.rotation, action.MatchBodyPart, new MatchTargetWeightMask(action.MatchPositionWeight, 0), action.MatchStartTime, action.MatchTargetTime);
    }
}
