using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    [SerializeField] UnityEvent shoot;

    // Recoil
    [Header("___________________Camera Recoil__________________")]

    public Transform recoilPosition;
    public Transform rotationPoint;
    [Space]
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

    Vector3 rotationalRecoil;
    Vector3 positionalRecoil;
    Vector3 _rotation;

    //animation
    public Animator animator;
    
    bool aiming;
    float heldDownTime = 0.1f;
    float currentHeldDownTime;

    private void Start()
    {
        Random.InitState(2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("Shooting");
            shoot?.Invoke();
        }
        else if (Input.GetMouseButton(0))
        {
            currentHeldDownTime += Time.deltaTime;
            if (currentHeldDownTime >= heldDownTime)
            {
                animator.Play("Shooting");
                shoot?.Invoke();

            }


        }
        else
        {
            currentHeldDownTime = 0;
        }
  


            UpdateRecoil();
    }

    private void UpdateRecoil()
    {
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, rotationalReturnSpeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, positionalReturnSpeed * Time.deltaTime);


        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, positionalRecoilSpeed * Time.deltaTime);
        _rotation = Vector3.Slerp(_rotation, rotationalRecoil, rotationalRecoilSpeed * Time.deltaTime);
        rotationPoint.localRotation = Quaternion.Euler(_rotation);
    }
        
    public void SetAiming(bool aiming)
    {
        this.aiming = aiming; 
    }
    public void Fire()
    {
        if (aiming)
        {
            rotationalRecoil += new Vector3(-recoilRotationAim.x, Random.Range(-recoilRotationAim.y, recoilRotationAim.y), Random.Range(-recoilRotationAim.z, recoilRotationAim.z));
            positionalRecoil += new Vector3(Random.Range(-recoilKickBackAim.x, recoilKickBackAim.x), Random.Range(-recoilKickBackAim.y, recoilKickBackAim.y), recoilKickBackAim.z);

        }
        else
        {
            rotationalRecoil += new Vector3(-recoilRotation.x, Random.Range(-recoilRotation.y, recoilRotation.y), Random.Range(-recoilRotation.z, recoilRotation.z));
            positionalRecoil += new Vector3(Random.Range(-recoilKickBack.x, recoilKickBack.x), Random.Range(-recoilKickBack.y, recoilKickBack.y), recoilKickBack.z);
        }
        
    }
}
