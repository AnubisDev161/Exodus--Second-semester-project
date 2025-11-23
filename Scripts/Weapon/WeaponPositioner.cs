using System;
using UnityEngine;
using UnityEngine.Events;

public class WeaponPositioner : MonoBehaviour, IWorldSwitchListener
{
    // aiming
    [SerializeField] UnityEvent<bool, float> ChangeFOVToAiming;

    [SerializeField] Transform defualtPosition;
    [SerializeField] Transform aimingPosition;
    [SerializeField] float aimAnimationSpeed = 10f;
    [SerializeField] private float zoomInDuration;
    [SerializeField] private float zoomOutDuration;
    [SerializeField] GunScript gun;


    private Quaternion originalGunRotation;
    private bool playerIsAiming;
    private bool inHellWorld;
    public static Action<bool> isAiming;

    private void OnEnable()
    {
        WorldSwitcher.switchWorld += OnSwitchWorld;
    }

    private void OnDisable()
    {
        WorldSwitcher.switchWorld -= OnSwitchWorld;
    }
    public void OnSwitchWorld(bool isInHellWorld)
    {
        inHellWorld = isInHellWorld;
    }

    private void Start()
    {
        originalGunRotation = gun.transform.localRotation;
        
    }

    private void Update()
    {
        if (Input.GetMouseButton(1) && inHellWorld && !gun.IsReloading)
        {
            transform.position = Vector3.Slerp(transform.position, aimingPosition.position, 1 / zoomInDuration * Time.deltaTime);
            gun.transform.localRotation = Quaternion.Slerp(gun.transform.localRotation, aimingPosition.localRotation, 1 / zoomInDuration * Time.deltaTime);

            if (!playerIsAiming)
            {
                playerIsAiming = true;
                ChangeFOVToAiming?.Invoke(true, zoomInDuration);
                isAiming?.Invoke(true);
            }
        }
        else
        {
            transform.position = Vector3.Slerp(transform.position, defualtPosition.position, 1 / zoomOutDuration * Time.deltaTime);
            gun.transform.localRotation = Quaternion.Slerp(gun.transform.localRotation, originalGunRotation, 1 / zoomInDuration * Time.deltaTime);
           
            if (playerIsAiming)
            {
                playerIsAiming = false;
                ChangeFOVToAiming?.Invoke(false, zoomOutDuration);
                isAiming?.Invoke(false);
            }
        }
    }
}
