using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{

    [Header("Physics")]
    public float MaxSpeed;
    public float BackwardsMovementSpeed;

    [Range(0f, 1f)]
    public float InAirControl;
    [field:SerializeField]public float TargetSpeed { get; set; }

    public float ActSpeed { get;  set; }

    public float Acceleration;
    public float Decceleration;
    public float DirectionalControl;

    public float InAirTimer { get;  set; }
    public bool CheckG { get;  set; }
    public bool CheckW { get;  set; }
    public float GroundTimer { get;  set; }
    public float AdjustmentAmt { get;  set; }

    [Header("Jumping")]
    public float JumpAmt;


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

    [Header("WallRun")]
    public float WallRunTime = 1;//how long we can run on walls
    public float ActWallRunTime { get; set; } = 0;//the timer for this
    public float WallRunUpwardsMovement = 4;//how much we run up walls
    public float WallRunSpeedAcceleration = 2f; // how quickly we build speed up walls



    [Header("Sliding")]
    public float PlayerCtrl;//how much control the player has during function like sliding
    [Tooltip("how fast we have to be traveling to slide")]
    public float SlideSpeedLimit;// how fast we have to be traveling to slide
    [Tooltip("how much we are pushed forward in slide")]
    public float SlideAmt;// how much we are pushed forward in slide
    public float SlideTimer { get; set; }
    public float maxSlideTime;

    [Header("Crouching")]
    public float crouchHeight;
    public float crouchSpeed;
    public float StandingHeight { get; set; }
    public CapsuleCollider CapsuleCollider { get; set; }

    [field:Header("Landing")]
    [field:SerializeField]public float MinimumDistanceToBeConsideredHardFall { get;private set; }

    [field:Header("Parkour Actions")]
    [field:SerializeField]public List<ParkourAction> ParkourActions { get; private set; }
    [SerializeField]public bool InAction { get; set; }

    public Rigidbody Rigidbody { get; private set; }
    [field:SerializeField]public Animator Animator { get; private set; }
    public InputManager InputManager { get; private set; }
    public PlayerCollision Collision { get; private set; }
    private PlayerMovementStateMachine movementStateMachine;
    public EnvironmentScanner EnvironmentScanner { get; private set; }

    private void Awake()
    {
        
        Rigidbody = GetComponent<Rigidbody>();
        InputManager = GetComponent<InputManager>();
        CapsuleCollider = GetComponent<CapsuleCollider>();
        Collision = GetComponent<PlayerCollision>();
        EnvironmentScanner = GetComponent<EnvironmentScanner>();
        movementStateMachine = new PlayerMovementStateMachine(this);
    }
    private void Start()
    {
        StandingHeight = CapsuleCollider.height;
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
