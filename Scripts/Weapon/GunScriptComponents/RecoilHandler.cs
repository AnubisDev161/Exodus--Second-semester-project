using UnityEngine;

public class RecoilHandler
{
    private Transform recoilPosition;
    private Transform rotationPoint;
    private float maxVerticalKick = 1f;
    private float maxHorizontalKick = 1f;
    public float verticalKick { get; private set;}
    public float horizontalKick { get; private set;}
    private float currentResetDelay;
    private Vector3 rotationalRecoil;
    private Vector3 positionalRecoil;
    private Vector3 _rotation;

    private WeaponData weaponData;
    
    public void UpdateKicksPerShot(WeaponData weaponData)
    {
        verticalKick += weaponData.verticalKickPerShot;
        verticalKick = Mathf.Clamp(verticalKick, 0, maxVerticalKick);

        horizontalKick += weaponData.horizontalKickPerShot;
        horizontalKick = Mathf.Clamp(horizontalKick, 0, maxHorizontalKick);

        currentResetDelay = 0;
    }
    
    public void AddRecoil(bool aiming, WeaponData weaponData)
    {
        if (aiming)
        {
            rotationalRecoil += new Vector3(verticalKick * -weaponData.recoilRotationAim.x, horizontalKick * UnityEngine.Random.Range(-weaponData.recoilRotationAim.y, weaponData.recoilRotationAim.y), 
                horizontalKick * UnityEngine.Random.Range(-weaponData.recoilRotationAim.z, weaponData.recoilRotationAim.z));

            positionalRecoil += new Vector3(verticalKick * UnityEngine.Random.Range(-weaponData.recoilKickBackAim.x, weaponData.recoilKickBackAim.x), 
                horizontalKick * UnityEngine.Random.Range(-weaponData.recoilKickBackAim.y, weaponData.recoilKickBackAim.y), horizontalKick * weaponData.recoilKickBackAim.z);
        }
        else
        {
            rotationalRecoil += new Vector3(horizontalKick * -weaponData.recoilRotation.x, horizontalKick + UnityEngine.Random.Range(-weaponData.recoilRotation.y, weaponData.recoilRotation.y), 
                verticalKick * UnityEngine.Random.Range(-weaponData.recoilRotation.z, weaponData.recoilRotation.z));

            positionalRecoil += new Vector3(horizontalKick * UnityEngine.Random.Range(-weaponData.recoilKickBack.x, weaponData.recoilKickBack.x),
                verticalKick * UnityEngine.Random.Range(-weaponData.recoilKickBack.y, weaponData.recoilKickBack.y), horizontalKick * weaponData.recoilKickBack.z);
        }
    }

    public void ReturnRecoil()
    {
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, weaponData.rotationalReturnSpeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, weaponData.positionalReturnSpeed * Time.deltaTime);
        
        recoilPosition.transform.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, weaponData.positionalRecoilSpeed * Time.deltaTime);
        _rotation = Vector3.Slerp(_rotation, rotationalRecoil, weaponData.rotationalRecoilSpeed * Time.deltaTime);
        rotationPoint.transform.localRotation = Quaternion.Euler(_rotation);
    }

    public void UpdateRecoilResetDelay(WeaponData weaponData)
    {
        currentResetDelay += Time.deltaTime;

        if (currentResetDelay >= weaponData.resetDelay)
        {
            verticalKick = 0;
            horizontalKick = 0;
            currentResetDelay = 0;
        }
    }
        //Debug.Log(currentResetDelay);
        //Debug.Log(rotationalRecoil);
        //Debug.Log(positionalRecoil);
        //Debug.Log(weaponData);
        
    

    public void Initialize(GameObject recoilPosition, GameObject rotationPoint, WeaponData weaponData)
    {
        this.recoilPosition = recoilPosition.transform;
        this.rotationPoint = rotationPoint.transform;
        this.weaponData = weaponData;
    }
    
    
}
