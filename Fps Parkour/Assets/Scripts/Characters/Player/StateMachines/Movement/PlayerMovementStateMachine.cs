using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public Player Player { get; }
    public PlayerIdlingState IdlingState { get;}
    public PlayerRunningState RunningState { get;}
    public PlayerVaultingState VaultingState { get;}
    public PlayerLightLandingState LightLandingState { get;}
    public PlayerHardLandingState HardLandingState { get;}
    public PlayerJumpState JumpState { get;}
    public PlayerInAirState InAirState { get;}
    
    public PlayerMovementStateMachine(Player player)
    {
        Player = player;

        IdlingState = new PlayerIdlingState(this);
        RunningState = new PlayerRunningState(this);
        VaultingState = new PlayerVaultingState(this);

        JumpState = new PlayerJumpState(this);
        InAirState = new PlayerInAirState(this);

        LightLandingState = new PlayerLightLandingState(this);
        HardLandingState = new PlayerHardLandingState(this);
    }
}
