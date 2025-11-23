using System;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

//public class PlayerMovement : MonoBehaviour, IWorldSwitchListener
//{
//    // Settings 
//    [SerializeField] private float jumpStrength = 10f;
//    [SerializeField] private float maxSpeed = 10f;
//    [SerializeField] private float mouseSensitivity = 30f;
//    [SerializeField] private float acceleration = 5f;
//    [SerializeField] private float deceleration = 5f;
//    [SerializeField] private float inHellTimeScale = 0.1f;

//    // References
//    [SerializeField] private GunScript gunPrefab;
//    [SerializeField] private Transform playerCamera;
//    [SerializeField] private Transform groundChecker;
//    [SerializeField] private InputActionAsset inputActionAsset;
    
//    // Variables
//    private Rigidbody playerRigidBody;
//    private InputAction moveAction;
//    private InputAction jumpAction;
//    private InputAction lookAction;
//    private InputAction shootAction;
//    private Vector2 moveAmount;
//    private Vector2 lookAmount;
//    private float cameraCap;
//    private bool isGrounded;
//    private float timeScale = 1;

//    private float forcePerFrame;
//    private float remainingForce;

//    void Awake()
//    {
//        // Initialise Objects
//        playerRigidBody = GetComponent<Rigidbody>();
//        moveAction = inputActionAsset.FindActionMap("Player").FindAction("Move");
//        jumpAction = inputActionAsset.FindActionMap("Player").FindAction("Jump");
//        lookAction = inputActionAsset.FindActionMap("Player").FindAction("Look");
//        shootAction = inputActionAsset.FindActionMap("Player").FindAction("Shoot");
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = true;
//    }
//    void OnEnable()
//    {
//        inputActionAsset.Enable();
//        jumpAction.Enable();
//        moveAction.Enable();
//        lookAction.Enable();
//        shootAction.Enable();
//        WorldSwitcher.switchWorld += OnSwitchWorld;
//    }
    
//    void OnDisable()
//    {
//        inputActionAsset.Disable();
//        jumpAction.Disable();
//        moveAction.Disable();
//        lookAction.Disable();
//        shootAction.Disable();
//        WorldSwitcher.switchWorld -= OnSwitchWorld;
//    }

//    private void OnDrawGizmosSelected()
//    {
//        if (groundChecker == null)
//        {
//            return;
//        }
        
//        // Draw cube for groundChecker.
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireCube(groundChecker.position, new Vector3(0.5f,0.1f,0.5f));
//    }

//    private void Update()
//    {
//        UpdateMouse();
//        UpdateMove();
//        TryJump();
//        TryShoot();
//        UpdateJumpForce();
//    }

//    private void UpdateJumpForce()
//    {
//        if (remainingForce > 0)
//        {
//            playerRigidBody.AddForce(Vector3.up * forcePerFrame, ForceMode.Impulse);
//            remainingForce -= forcePerFrame;
//        }
//    }

//    private void UpdateMove()
//    {
//        // Movement function
//        moveAmount = moveAction.ReadValue<Vector2>();
        
//        if (moveAction.IsPressed())
//        {
            
//            Vector3 direction = transform.right * moveAmount.x + transform.forward * moveAmount.y;
//            direction = direction.normalized;
           
//            playerRigidBody.linearVelocity = Vector3.MoveTowards(playerRigidBody.linearVelocity, direction * maxSpeed, acceleration * Time.fixedDeltaTime * timeScale);
//        }
//        else
//        {
//            if (isGrounded)
//            {
//                playerRigidBody.linearVelocity = Vector3.MoveTowards(playerRigidBody.linearVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime * timeScale);
//            }
//        }
//    }

//    private void TryShoot()
//    {
//        // Checks if player is holding a semi or automatic gun.
//        if (shootAction.IsPressed() && gunPrefab.GetGunType == GunType.Automatic)
//        {
//            PullTheTrigger();
//        }
//        else if (shootAction.WasPressedThisFrame() && gunPrefab.GetGunType == GunType.Semi)
//        {
//            PullTheTrigger();
//        }
//        else
//        {
//            return;
//        }
//    }
//    private void TryJump()
//    {
//        isGrounded = Physics.CheckBox(groundChecker.position, new Vector3(0.5f, 0.1f ,0.5f) / 2, groundChecker.rotation);
//        if (isGrounded && jumpAction.WasPressedThisFrame())
//        {
//            Jump();
//        }
//    }
//    private void Jump()
//    {
//        //playerRigidBody.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
//        if (timeScale != 1)
//        {
//            forcePerFrame = jumpStrength * (Time.fixedDeltaTime / timeScale);
//            remainingForce = jumpStrength;
//        }
//        else
//        {
//            playerRigidBody.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
//        }


//    }
    
//    private void PullTheTrigger()
//    {
//        gunPrefab.Shoot();
//    }

//    private void UpdateMouse()
//    {
//        lookAmount = lookAction.ReadValue<Vector2>();
//        cameraCap -= lookAmount.y * mouseSensitivity;
//        cameraCap = Mathf.Clamp(cameraCap, -90f, 90f);
//        playerCamera.localEulerAngles = Vector3.right * cameraCap;
//        var newRotation = playerRigidBody.rotation.eulerAngles;
//        newRotation.y += lookAmount.x * mouseSensitivity;
//        playerRigidBody.rotation =  Quaternion.Euler(newRotation);
//    }

//    public void OnSwitchWorld(bool isInHellWorld)
//    {
//        if (isInHellWorld)
//        {
//            timeScale = inHellTimeScale;
//            gunPrefab.gameObject.SetActive(true);
//        }
//        else
//        {
//            timeScale = 1;
//            gunPrefab.gameObject.SetActive(false);
//        }
//    }
//}
