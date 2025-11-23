using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "WeaponData", menuName = "_____Custom Menu____/Weapon Data", order = 1)]
public class WeaponData : ScriptableObject
{

    [field: Header("_____Weapon Stats_____")]
    [field: SerializeField] public string weaponName { get; private set; } = "Negev";
    [field: SerializeField] public GunType gunType  { get; private set; } = GunType.Automatic;
    [field: SerializeField] public float maxXSpread { get; private set; } = 3.5f;
    
    [field: SerializeField] public float maxYSpread { get; private set; } = 3.5f;
    [field: SerializeField] public float fireRate { get; private set; } = 0.1f;
    
    [field: SerializeField] public float equipTime { get; private set; } = 0.3f;
    [field: Space]
    
    
    [field: Header("_____Magazine Stats_____")]
    [field: SerializeField] public float reloadTime { get; private set; } = 2f;
    [field: SerializeField] public int magazineCapacity { get; private set; } = 20;
    [field: SerializeField] public int currentAmmo { get; private set; } = 20;
    [field: SerializeField] public int ammoInReserve { get; private set; } = 50;
    [field: SerializeField] public int maxAmmo { get; private set; } = 14;
    [field: SerializeField] public bool infiniteAmmo { get; private set; } = false;
    [field: Space]
    
    
    [field: Header("_____Bullet Stats_____")]
    [field: SerializeField] public int damage { get; private set; } = 10;
    [field: SerializeField] public int bulletsPerShot { get; private set; } = 1;
    [field: SerializeField] public int bulletSpeed { get; private set; } = 1;
    [field: Space]
    
    
    [field: Header("_____Camera Recoil_____")]
    [field: SerializeField] public float positionalRecoilSpeed { get; private set; } = 8f;
    [field: SerializeField] public float rotationalRecoilSpeed { get; private set; } = 8f;
    [field: SerializeField] public float positionalReturnSpeed { get; private set; } = 18f;
    [field: SerializeField]  public float rotationalReturnSpeed { get; private set; } = 36f;
    
    [field: Space]
    [field: SerializeField]  public Vector3 recoilRotation { get; private set; } = new Vector3(10, 5f, 7f);
    [field: SerializeField] public Vector3 recoilKickBack { get; private set; } = new Vector3(0.015f, 0f, -0.2f);

    [field: Space]
    public Vector3 recoilRotationAim { get; private set; } = new Vector3(10, 4, 6);
    public Vector3 recoilKickBackAim { get; private set; } = new Vector3(0.015f, 0f, -0.2f);
    
    [field: Space]
    [field: SerializeField] public float resetDelay { get; private set; } = 0.2f;
    [field: SerializeField] public float horizontalKickPerShot { get; private set; } = 0.2f;
    [field: SerializeField] public float verticalKickPerShot { get; private set; } = 0.2f;
    
}