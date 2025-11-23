using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
public class WeaponHolder : MonoBehaviour
{

   


    [SerializeField] private InputActionAsset inputActionAsset;
    public float mouseSensitivity = 30f;
    public InputAction lookAction;



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

    private void OnEnable()
    {

        lookAction.Enable();

    }

    private void OnDisable()
    {

        lookAction.Disable();
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
        if (isRecoiling)
        {
            recoilStoppingPosition = Vector3.zero;

        }
        else
        {
            recoilStoppingPosition = currentRotation;
        }
        currentRotation = Vector3.Lerp(currentRotation, recoilStoppingPosition, returnSpeed * Time.deltaTime);

        if (currentRotation == recoilStoppingPosition)
        {
            isRecoiling = false;
        }
        _rotation = Vector3.Slerp(_rotation, currentRotation, rotationSpeed * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(_rotation);


    }


    public void OnShoot()
    {
        currentRotation += new Vector3(-recoilRotation.x, UnityEngine.Random.Range(-recoilRotation.y, recoilRotation.y), UnityEngine.Random.Range(-recoilRotation.z, recoilRotation.z));
        isRecoiling = true;
    }
    
}
