using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using System;

[RequireComponent (typeof(Animation))]
[RequireComponent(typeof(Volume))]
public class PlayerCamera : MonoBehaviour, IWorldSwitchListener
{
    private Volume volume;
    private Animation hellWorldAnimation;
    private Camera cam;
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private float shakeStrength;
    [SerializeField] private float shakeDuration;
    public float mouseSensitivity = 30f;
    public Transform playerTransform;
    public InputAction lookAction;
    float xRotation = 0f;


    // recoil settings
    public float rotationSpeed = 6;
    public float returnSpeed = 25;

    public Vector3 recoilRotation = new Vector3(2f, 2f, 2f);
    public Vector3 recoilRotationAiming = new Vector3(0.5f, 0.5f, 1.5f);

    public bool aiming;
    private bool isRecoiling;

    private Vector3 currentRotation;
    private Vector3 _rotation;


    private void Awake()
    {
        lookAction = inputActionAsset.FindActionMap("Player").FindAction("Look");
    }
    private void Start()
    {

        volume = GetComponent<Volume>();
        hellWorldAnimation = GetComponent<Animation>();
        volume.enabled = false;
        cam = GetComponent<Camera>();

        // mouse curser
      //  Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable()
    {
        WorldSwitcher.switchWorld += OnSwitchWorld;
        lookAction.Enable();

    }

    private void OnDisable()
    {
        WorldSwitcher.switchWorld -= OnSwitchWorld;
        lookAction.Disable();
    }
    public void OnSwitchWorld(bool isInHellWorld)
    {
        volume.enabled = isInHellWorld;
        hellWorldAnimation.enabled = isInHellWorld;
        
    }

    private void Update()
    {
        LookAround();
    }

    private void LookAround()
    {
        float mouseX = lookAction.ReadValue<Vector2>().x;
        float mouseY = lookAction.ReadValue<Vector2>().y;

        currentRotation.x -= mouseY;
        currentRotation.x = math.clamp(currentRotation.x, -90, 90f);

        Vector3 recoilStoppingPosition;
        if(isRecoiling)
        {
            recoilStoppingPosition = Vector3.zero;

        }
        else
        {
            recoilStoppingPosition = currentRotation;
        }
        currentRotation = Vector3.Lerp(currentRotation, recoilStoppingPosition, returnSpeed * Time.deltaTime);
        
        if(currentRotation == recoilStoppingPosition)
        {
            isRecoiling = false;
        }
        _rotation = Vector3.Slerp(_rotation, currentRotation, rotationSpeed * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(_rotation);
        playerTransform.Rotate(Vector3.up * mouseX);

        // 

    }

    public void OnShoot()
    {
        currentRotation += new Vector3(-recoilRotation.x, UnityEngine.Random.Range(-recoilRotation.y, recoilRotation.y), UnityEngine.Random.Range(-recoilRotation.z, recoilRotation.z));
        isRecoiling = true;
    }
}

