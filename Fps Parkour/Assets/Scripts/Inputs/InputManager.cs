using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerInput input;

    public event Action JumpEvent;
    //public event Action SprintEvent;
    public event Action CrouchEvent;
    public event Action CrouchCancelEvent;
    //public event Action WalkEvent;
    public event Action FireEvent;
    public event Action FireCancelEvent; 
    public event Action MapOpenEvent;
    public event Action ScoreboardOpenEvent;
    public event Action ScoreboardClosedEvent;
    public event Action InteractStarted;
    public event Action InteractCanceled;
    public event Action MovingStarted;
    public event Action MovingCanceled;
    //public event Action ScopeEvent;
    //public event Action ScopeCancelEvent;
    public event Action ReloadEvent;
    
    public Vector2 move;
    public Vector2 look;
    public float moveSmoothTimeSpeed = 0.2f;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MovingStarted?.Invoke();
        }
        move = context.ReadValue<Vector2>();
        if (context.canceled)
        {
            MovingCanceled?.Invoke();
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {

        look = context.ReadValue<Vector2>();

    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpEvent?.Invoke();
        }

    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CrouchEvent?.Invoke();
        }
        if (context.canceled)
        {
            CrouchCancelEvent?.Invoke();
        }
    }
    //public void OnSprint(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        SprintEvent?.Invoke();
    //    }

    //}
    //public void OnWalk(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        WalkEvent?.Invoke();
    //    }

    //}
    //public void OnScope(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        ScopeEvent?.Invoke();
    //    }
    //    if (context.canceled)
    //    {
    //        ScopeCancelEvent?.Invoke();
    //    }

    //}
    public void OnMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MapOpenEvent?.Invoke();
        }
        
    }
    public void OnScoreboard(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ScoreboardOpenEvent?.Invoke();
        }
        if (context.canceled)
        {
            ScoreboardClosedEvent?.Invoke();
        }
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            FireEvent?.Invoke();
        }
        if (context.canceled)
        {
            FireCancelEvent?.Invoke();
        }

    }
    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ReloadEvent?.Invoke();
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InteractStarted?.Invoke();
        }
        if (context.canceled)
        {
            InteractCanceled?.Invoke();
        }
    }
}
