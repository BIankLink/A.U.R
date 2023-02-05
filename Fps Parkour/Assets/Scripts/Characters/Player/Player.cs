using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Physics")]
    public float MaxSpeed;
    public float BackwardsMovementSpeed;

    [Range(0f, 1f)]
    public float InAirControl;
    public float TargetSpeed { get; set; }

    public float ActSpeed { get;  set; }

    public float Acceleration;
    public float Decceleration;
    public float DirectionalControl;

    public float InAirTimer { get;  set; }
    public bool CheckG { get;  set; }
    public float GroundTimer { get;  set; }
    public float AdjustmentAmt { get;  set; }

    [Header("Turning")]
    public float TurnSpeed;
    public float TurnSpeedOnAir;
    public float TurnSpeedOnWalls;

    public float LookUpSpeed;
    public Camera Head;

    public float Yturn { get; set; }
    public float Xturn { get; set; }
    public float MaxLookAngle;
    public float MinLookAngle;

    [Header("Sliding")]
    public float PlayerCtrl;//how much control the player has during function like sliding

    [field:Header("Landing")]
    [field:SerializeField]public float MinimumDistanceToBeConsideredHardFall { get;private set; }

    [field:Header("Parkour Actions")]
    [SerializeField]public List<ParkourAction> ParkourActions { get; private set; }
    [SerializeField]public bool InAction { get; set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public InputManager InputManager { get; private set; }
    public PlayerCollision Collision { get; private set; }
    private PlayerMovementStateMachine movementStateMachine;
    public EnvironmentScanner EnvironmentScanner { get; private set; }

    private void Awake()
    {
        
        Rigidbody = GetComponent<Rigidbody>();
        InputManager = GetComponent<InputManager>();
        Collision = GetComponent<PlayerCollision>();
        EnvironmentScanner = GetComponent<EnvironmentScanner>();
        movementStateMachine = new PlayerMovementStateMachine(this);
    }
    private void Start()
    {
        AdjustmentAmt = 1;
        movementStateMachine.ChangeState(movementStateMachine.IdlingState);
    }
    private void Update()
    {
        movementStateMachine.HandleInput();
        movementStateMachine.Update(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        LookUpDown(InputManager.look.y,Time.deltaTime);
        movementStateMachine.PhysicsUpdate(Time.deltaTime);
    }
    void LookUpDown(float yAmt,float deltaTime)
    {
        Xturn -= (yAmt * deltaTime) * LookUpSpeed;
        Xturn = Mathf.Clamp(Xturn, MinLookAngle, MaxLookAngle);
        Head.transform.localRotation = Quaternion.Euler(Xturn,0,0);
    }
}
