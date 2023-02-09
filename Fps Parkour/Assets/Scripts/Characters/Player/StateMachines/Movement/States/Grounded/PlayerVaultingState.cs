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
       /* if (stateMachine.Player.hitData.forwardHitFound)
        {
            foreach (ParkourAction action in stateMachine.Player.ParkourActions)
            {


                if (action.CheckIfPossible(stateMachine.Player.hitData, stateMachine.Player.transform))
                {

                    stateMachine.Player.StartCoroutine(Vaulting(action));
                }
            }
        }*/
        // Debug.Log(stateMachine.Player.hitData.heightHit.point);

    }
    public override void Update(float deltaTime)
    {
        //base.Update(deltaTime);
        stateMachine.Player.Rigidbody.velocity = Vector3.zero;

    }
    public override void PhysicsUpdate(float deltaTime)
    {
        //base.PhysicsUpdate(deltaTime);
        LedgeGrab(deltaTime);
    }
    void LedgeGrab(float deltaTime)
    {
        //tick ledge grab time 
        stateMachine.Player.ActPullTm += deltaTime;

        //pull up the ledge
        float PullUpLerp = stateMachine.Player.ActPullTm / stateMachine.Player.PullUpTime;

        if (PullUpLerp < 0.5)
        {
            //lerp our player upwards to the leges y position
            float LAmt = PullUpLerp * 2;
            stateMachine.Player.transform.position = Vector3.Lerp(stateMachine.Player.OrigPos, new Vector3(stateMachine.Player.OrigPos.x, stateMachine.Player.LedgePos.y, stateMachine.Player.OrigPos.z), LAmt);
        }
        else if (PullUpLerp <= 1)
        {
            //set new pull up position
            if (stateMachine.Player.OrigPos.y != stateMachine.Player.LedgePos.y)
                stateMachine.Player.OrigPos = new Vector3(stateMachine.Player.transform.position.x, stateMachine.Player.LedgePos.y, stateMachine.Player.transform.position.z);


            //move to the ledge position
            float LAmt = (PullUpLerp - 0.5f) * 2;
            stateMachine.Player.transform.position = Vector3.Lerp(stateMachine.Player.OrigPos, stateMachine.Player.LedgePos, PullUpLerp);
        }
        else
        {
            //we have finished pulling up!
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }

   /* protected IEnumerator Vaulting(ParkourAction action)
    {
        stateMachine.Player.Animator.applyRootMotion = true;
        stateMachine.Player.InAction = true;
        stateMachine.Player.Rigidbody.useGravity = false;
        stateMachine.Player.CapsuleCollider.enabled = false;
        stateMachine.Player.InputManager.input.actions.Disable();
        stateMachine.Player.Animator.SetTrigger(action.AnimationName);
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

            if (stateMachine.Player.Animator.IsInTransition(0) && timer > 0.5f)
                break;
            while (stateMachine.Player.Animator.IsInTransition(0))
            {
                yield return null;
            }
            if (action.EnableTargetMatching)
            {
                MatchTarget(action);
            }
           


            yield return null;
        }
        yield return new WaitForSeconds(action.PostActionDelay);
        stateMachine.Player.InputManager.input.actions.Enable();
        stateMachine.Player.CapsuleCollider.enabled = true;
        stateMachine.Player.Rigidbody.useGravity=true;
        stateMachine.Player.InAction = false;
        stateMachine.ChangeState(stateMachine.IdlingState);
    }
    protected void MatchTarget(ParkourAction action)
    {
        if (stateMachine.Player.Animator.isMatchingTarget) { return; }
        
        //float normalizeTime = Mathf.Repeat(stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f);

        //if (normalizeTime > action.MatchTargetTime)
        //    return;
        
        stateMachine.Player.Animator.MatchTarget(action.MatchPosition, stateMachine.Player.transform.rotation, action.MatchBodyPart, new MatchTargetWeightMask(action.MatchPositionWeight, 0), action.MatchStartTime, action.MatchTargetTime);
    }*/
}
