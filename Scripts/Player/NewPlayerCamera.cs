using System;
using UnityEngine;
using UnityEngine.Rendering;

public class NewPlayerCamera : MonoBehaviour
{

    private Camera cam;

    // recoil settings
    [SerializeField] float rotationSpeed = 6;
    [SerializeField] float returnSpeed = 25;
    float MaxVerticalKick = 1;
    [SerializeField] float verticalKickPerShot;
    float MaxHorizontalKick = 1;
    [SerializeField] float horizontalKickPerShot;
    float verticalKick;
    float horizontalKick;

    [SerializeField] Vector3 recoilRotation = new Vector3(2f, 2f, 2f);

    private Vector3 currentRotation;
    private Vector3 _rotation;

    [SerializeField] Transform defaultPosition;

    [SerializeField] float resetDelay = 0.2f;
    private float currentResetDelay;

    [SerializeField] float defaultFOV = 90;
    [SerializeField] float aimedFOV = 75;
    private float currentFOV;
    private float currentZoomInDuration;

    private void Awake()
    {
        currentFOV = defaultFOV;
        currentZoomInDuration = 0.3f;
    }
    private void Update()
    {
        UpdateRecoil();
        UpdateRecoilResetDelay();
        UpdateFOV();
    }

    private void UpdateFOV()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, currentFOV, 1 / currentZoomInDuration * Time.deltaTime);
    }

    private void UpdateRecoilResetDelay()
    {
        currentResetDelay += Time.deltaTime;

        if (currentResetDelay >= resetDelay)
        {
            verticalKick = 0;
            horizontalKick = 0;
            currentResetDelay = 0;
        }
    }

    public void OnShoot()
    {
        verticalKick += verticalKickPerShot * Time.deltaTime;
        verticalKick = Mathf.Clamp(verticalKick, 0, MaxVerticalKick);

        horizontalKick += horizontalKickPerShot * Time.deltaTime;
        horizontalKick = Mathf.Clamp(horizontalKick, 0, MaxHorizontalKick);

        // add new recoil 
        currentRotation += new Vector3(verticalKick * -recoilRotation.x, verticalKick * UnityEngine.Random.Range(-recoilRotation.y, recoilRotation.y), horizontalKick * UnityEngine.Random.Range(-recoilRotation.z, recoilRotation.z));
        
        // reset the recoil reset time
        currentResetDelay = 0;
    }

    public void OnAim(bool playerIsAiming, float currentZoomInDuration)
    {
        this.currentZoomInDuration = currentZoomInDuration;

        if (playerIsAiming)
        {
            currentFOV = aimedFOV;
        }
        else
        {
            currentFOV = defaultFOV;
        }
    }

    private void UpdateRecoil()
    { 
        // lerp the current rotation to vector zero to ensure that the camera always returns to its initial position
        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        _rotation = Vector3.Slerp(_rotation, currentRotation, rotationSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(_rotation);
        
    }

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

  

}
