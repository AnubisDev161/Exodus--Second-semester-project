using UnityEngine;

[System.Serializable]
public class CameraRecoil
{
    //[SerializeField] GameObject parent;
    //public Transform recoilPosition;
    //public Transform rotationPoint;
    
    public float positionalRecoilSpeed = 8f;
    public float rotationalRecoilSpeed = 8f;

    public float positionalReturnSpeed = 18f;
    public float rotationalReturnSpeed = 36f;

    [Space]
    public Vector3 recoilRotation = new Vector3(10, 5f, 7f);
    public Vector3 recoilKickBack = new Vector3(0.015f, 0f, -0.2f);
    [Space]
    public Vector3 recoilRotationAim = new Vector3(10, 4, 6);
    public Vector3 recoilKickBackAim = new Vector3(0.015f, 0f, -0.2f);
    [HideInInspector] public float maxVerticalKick = 1;
    [Space]
    [SerializeField] public float verticalKickPerShot;
    [HideInInspector] public float maxHorizontalKick = 1;
    [SerializeField] public float horizontalKickPerShot;
    [HideInInspector] public float verticalKick;
    [HideInInspector] public float horizontalKick;

    [SerializeField] public float resetDelay = 0.2f;
    [HideInInspector] public float currentResetDelay;
    [HideInInspector] public Vector3 rotationalRecoil;
    [HideInInspector] public Vector3 positionalRecoil;
    [HideInInspector] public Vector3 _rotation;
    
    
    // // Getter 
    // public float PositionalRecoilSpeed => positionalRecoilSpeed;
    // public float RotationalRecoilSpeed => rotationalRecoilSpeed;
    // public float PositionalReturnSpeed => positionalReturnSpeed;
    // public float RotationalReturnSpeed => rotationalReturnSpeed;




}

