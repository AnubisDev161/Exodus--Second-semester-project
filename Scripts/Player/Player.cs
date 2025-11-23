using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IWorldSwitchListener
{
    // Settings 
    [Header("___________Movement___________")]
    [Space]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float sprintSpeed = 14f;

    private float currentSpeed { get; set; } = 12f;
    private Vector2 lastInputDirection= Vector2.zero;
    public bool isSprinting { get; set; }

    [SerializeField] private float decceleration = 5f;
    [SerializeField] public float gravity = -9.81f;

    [SerializeField] AnimationCurve walkingAccelerationCurve;
    [SerializeField] AnimationCurve sprintingAccelerationCurve;

    [SerializeField] private float sprintingAccelerationTime;
    [SerializeField] private float walkingAccelerationTime;
    [Space]
    [SerializeField] public float jumpHeight = 10f;
    [SerializeField] public float doubleJumpHeight = 10f;
    [SerializeField] public float coyoteTime { get; private set; } = 0.2f;
    
    public float leftGroundtime { get; private set; } = 0;
    public float doubleJumpCooldown = 5;
    public float jumpCooldown = 0.2f;
    public float fallingEffectTriggertime;
    public float currentJumpCooldown { get; set; }
    public float currentDoubleJumpCooldown { get; set; }
    public bool isDoublejumping { get;  set; }
    
    [SerializeField] private float inHellTimeScale = 0.1f;
    [Space]

    // Footsteps
    private float footstepTimer = 0.3f;
    private float currentFootstepTimer = 0f;
    
    
    // References
    [SerializeField] private CharacterController characterController;

    //Actions and events
    [SerializeField] UnityEvent<bool> EnableGun;
    public UnityEvent<bool, bool> EnableHeadBobbing;
    public UnityEvent TriggerFallingEffect;

    // physics
    [SerializeField] public Vector3 velocity;
    [SerializeField] Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;
    private Vector2 inputDirection;
    private Interactable interactableInRange;
    [SerializeField] private float maxWalkSpeed;
    [SerializeField] private float maxSprintSpeed;

    public bool hasTriggeredJumpingPad {  get; set; }

    public bool isGrounded { get; private set; }
    private float timeScale { get; set; } = 1;

    float moveTime;
    private bool inTutorial;

    // properties that need to be accessed by the state machine
    public float Speed
    {
        get { return speed; }

    }
    public float RunSpeed
    {
        get { return sprintSpeed; }

    }

    // State machine section
    public PlayerStateMachine stateMachine { get; set; }
    public PlayerIdleState idleState { get; set; }
    public PlayerIdleInHellState idleInHellState { get; set; }
    public PlayerWalkState walkState { get; set; }
    public PlayerWalkInHellState walkInHellState { get; set; }
    public PlayerRunState runState { get; set; }
    public PlayerJumpState jumpState { get; set; }
    public PlayerFallState fallState { get; set; }
    public PlayerFallInHellState fallInHellState { get; set; }


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine);
        idleInHellState = new PlayerIdleInHellState(this, stateMachine);
        walkState = new PlayerWalkState(this, stateMachine);
        walkInHellState = new PlayerWalkInHellState(this, stateMachine);
        runState = new PlayerRunState(this, stateMachine);
        jumpState = new PlayerJumpState(this, stateMachine);
        fallInHellState = new PlayerFallInHellState (this, stateMachine);
        fallState = new PlayerFallState(this, stateMachine);
        
    }


    private void OnInTutorial(bool inTutorial)
    {
        this.inTutorial = inTutorial;
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);

        // mouse curser
        Cursor.lockState = CursorLockMode.Locked;
    }
    void OnEnable()
    {
        WorldSwitcher.switchWorld += OnSwitchWorld;
        PlayerInputController.inTutorial += OnInTutorial;
    }

    void OnDisable()
    {
        WorldSwitcher.switchWorld -= OnSwitchWorld;
        PlayerInputController.inTutorial -= OnInTutorial;
    }

    private void Update()
    {
        if (inTutorial) return;

        stateMachine.currentState.FrameUpdate();
        UpdateDoubleJumpCooldown();
        UpdateLeftGroundTime();
        UpdateIsGrounded();

    }

    private void UpdateLeftGroundTime()
    {
        if(!isGrounded)
        {
            leftGroundtime += Time.deltaTime;
        }
        else
        {
            leftGroundtime = 0;
        }
        
    }

    public bool TryMove()
    {
        inputDirection = PlayerInputController.Instance.MoveAction.ReadValue<Vector2>();

        if (inputDirection != Vector2.zero)
        {
            return true;
        }

        return false;
    }

    public void UpdateMove()
    {
        // Movement function 
        inputDirection = PlayerInputController.Instance.MoveAction.ReadValue<Vector2>().normalized;
        
        currentFootstepTimer += Time.deltaTime * (currentSpeed / 10);
        if (inputDirection != Vector2.zero && isGrounded)
        {
            PlayFootsteps();
        }
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            
        }

        UpdateAccelerationAndDecceleration();

        
        Vector2 _inputDirection = inputDirection;

        if (inputDirection == Vector2.zero)
        {
            _inputDirection = lastInputDirection;
        }

        Vector3 move = (transform.right * _inputDirection.x + transform.forward * _inputDirection.y).normalized * currentSpeed;

        characterController.Move(move * Time.deltaTime * timeScale);

        velocity.y += gravity * Time.deltaTime * timeScale;

        characterController.Move(velocity * Time.deltaTime * timeScale);
        
        
    }

    private void UpdateAccelerationAndDecceleration()
    {
        if (inputDirection.magnitude > 0)
        {
            // if there is input, add timeDelta to know how long the player is already accelerating
            moveTime += Time.deltaTime;
            // last input needs to be saved to avoid a multiplication with 0 when the player is supposed to deccelerate
            lastInputDirection = inputDirection;
            calculateAcceleration();
        }
        else
        {
            calculateDecceleration();
        }
    }

    private void calculateDecceleration()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, 0, decceleration * Time.deltaTime);
        moveTime = Mathf.Lerp(moveTime, 0, decceleration * Time.deltaTime);
    }

    private void calculateAcceleration()
    {
        // adjust max speed depending on if the player is walking or sprinting
        float maxSpeed = maxWalkSpeed;
        if (isSprinting)
        {
            maxSpeed = maxSprintSpeed;
        }

        // acceleration
        if (isSprinting)
        {
            moveTime = Mathf.Clamp(moveTime, 0, sprintingAccelerationTime);
            currentSpeed = sprintingAccelerationCurve.Evaluate(moveTime);
        }
        else
        {
            moveTime = Mathf.Clamp(moveTime, 0, walkingAccelerationTime);
            currentSpeed = walkingAccelerationCurve.Evaluate(moveTime);
        }
    }

    private void UpdateIsGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    public bool TryJump()
    {
        if ((isGrounded || leftGroundtime <= coyoteTime) && PlayerInputController.Instance.JumpAction.WasPressedThisFrame() )
        {
            return true;
        }
        return false;
    }
    
    private void PlayFootsteps()
    {
        if (currentFootstepTimer >= footstepTimer)
        {
            SoundManager.PlaySound(SoundType.FOOTSTEPS);
            currentFootstepTimer = 0f;
        }
    }

    public bool TryRun()
    {
        if (PlayerInputController.Instance.SprintAction.IsPressed() && isGrounded)
        {
            return true;
        }
        return false;
    }

    public bool TryDoubleJump()
    {
        if (PlayerInputController.Instance.JumpAction.WasPressedThisFrame() && currentDoubleJumpCooldown == 0)
        {
            return true;
            
        }
        return false;
    }
    
    public void OnSwitchWorld(bool isInHellWorld)
    {
        if (isInHellWorld)
        {
            timeScale = inHellTimeScale;
            EnableGun?.Invoke(true);
            
            stateMachine.ChangeState(idleInHellState);
        }
        else
        {
            timeScale = 1;
            EnableGun?.Invoke(false);
            stateMachine.ChangeState(idleState);
        }
    }

    private void UpdateDoubleJumpCooldown()
    {
        if (currentDoubleJumpCooldown > 0)
        {
            currentDoubleJumpCooldown -= Time.deltaTime;
        }
        else
        {
            currentDoubleJumpCooldown = 0;
        }
    }

    public void UpdateJumpCooldown()
    {
        if (currentJumpCooldown > 0)
        {
            currentJumpCooldown -= Time.deltaTime;
        }
        else
        {
            currentJumpCooldown = 0;
        }
    }

    public void OnAim(bool isAiming)
    {
        if(isAiming && stateMachine.currentState == runState)
        {
            stateMachine.ChangeState(walkState);
        }
    }

    public void InteractableInRange(Interactable interactable)
    {
        interactableInRange = interactable;
    }

    private void Interact()
    {
        if (interactableInRange == null) return; 
        interactableInRange.Trigger();
    }

    public void CheckForInteractableInRange()
    {
        if (PlayerInputController.Instance.Interact.WasPressedThisFrame())
        {
            Interact();
        }
    }

    public void JumpPadTriggerd(float jumpForce)
    {
        if(stateMachine.currentState == runState || stateMachine.currentState == idleState || stateMachine.currentState == walkState)
        {
            hasTriggeredJumpingPad = true;
            stateMachine.ChangeState(jumpState);
            velocity.y = Mathf.Sqrt(doubleJumpHeight * jumpForce * -2f * gravity);
        }
    }
}


