using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(WeaponAnimatonController))]
public class GunScript : Subject, IWorldSwitchListener
{
    [Header("___________________Weapon Data__________________")]
    [SerializeField, Tooltip("Create Weapon Data in Menu (Create/Custom Menu/Weapon Data )")]
    private WeaponData weaponData;
    //private RecoilHandler recoilHandler = new RecoilHandler();
    
    [Header("_____________________Objects____________________")]
    [SerializeField] private RaycastBullet raycastBullet;
    [SerializeField] private GameObject bulletSpawnPoint;
    [SerializeField] private UnityEvent shoot;
    [SerializeField] private UnityEvent<bool> enableCrystal;

    private WeaponAnimatonController animator;

    public static Action reloading;
    public static Action<float, float> shot;
    public static Action startAnimationFinished;
    public static Action<int, int> updateCrystalInWeapon;
    private int maxAmmo;

    // Real time changes
    private int currentAmmo;
    private bool isReloading;
    private bool isEquippingOrUnequipping = false;
    private bool isCoroutineRunning;
    private float currentEquipTime;
    
    public int ammoReserve { get; set; }
    private float currentReloadTime;
    private float xSpread;
    private float ySpread;
    [SerializeField] private LayerMask opponentLayer;
    private string currentShootAnimation = "Shooting1";
    private bool playHeavenAnimationAfterReloading = false;
    [SerializeField] private GameObject gunModel;

    // tutorial variables
    private bool inStartAnimation;
    // weapon Cooldown
    private float nextFireTime;

    // world switch
    private bool isInHellWorld;
    
    // Getter
    public GunType GetGunType => weaponData.gunType;
    public int MagazineCapacity => weaponData.magazineCapacity;
    public int CurrentAmmo => currentAmmo;
    public string WeaponName => weaponData.weaponName;
    public float CurrentReloadTime => currentReloadTime;
    public float ReloadTime => weaponData.reloadTime;
    public bool IsReloading => isReloading;
    public int AmmoReserve =>  ammoReserve;
    public int MaxAmmo => maxAmmo;


    public void Start()
    {
        Initialize();
        OnSwitchWorld(isInHellWorld);
    }

    private void OnEnable()
    {
        WorldSwitcher.switchWorld += OnSwitchWorld;
        TutorialHandler.triggerStartAnimation += OnTriggerStartAnimation;
   
    }

    private void OnDisable()
    {
        WorldSwitcher.switchWorld -= OnSwitchWorld;
        TutorialHandler.triggerStartAnimation -= OnTriggerStartAnimation;
    }

    public void Update()
    {
        nextFireTime -= Time.deltaTime;
        
        if (isEquippingOrUnequipping)
        {
            currentEquipTime -= Time.deltaTime;
            

            if (currentEquipTime <= 0)
            {
                isEquippingOrUnequipping = false;
                //Debug.Log("Equipped");
            }
        }
    }
    
    private void FireBullets()
    { 
        for (int i = 0; i < weaponData.bulletsPerShot; i++) // bullets per shot
        {
            ySpread = weaponData.maxYSpread; 
            
            var newBullet = Instantiate(raycastBullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
            newBullet.Fire(xSpread, ySpread, opponentLayer, weaponData.damage); 
        }


        float cooldown = weaponData.fireRate;
        
        if (!weaponData.infiniteAmmo)
        {
            currentAmmo--;
        }

        nextFireTime = cooldown;
        animator.ForcePlay("Shooting");
       
        SoundManager.PlaySound(SoundType.SHOOT);
        
    }
    
    public void Shoot()
    {
        // fire Rate & current ammo check
        if (nextFireTime >= 0 || currentAmmo == 0 || !gameObject.active || isEquippingOrUnequipping) return;
        FireBullets();
        shoot?.Invoke();
        UpdateAmmo(true);
       

        if (currentAmmo <= 0)
        {
            Reload();
        }

        updateCrystalInWeapon?.Invoke(currentAmmo, ammoReserve);
        shot?.Invoke(ammoReserve, currentAmmo);

    }
    
    private void Initialize()
    {
        maxAmmo = weaponData.maxAmmo;
        currentAmmo = weaponData.currentAmmo;
        ammoReserve = weaponData.ammoInReserve;
        xSpread = weaponData.maxXSpread;
        ySpread = weaponData.maxYSpread;
        animator = GetComponent<WeaponAnimatonController>();
        updateAmmo?.Invoke(currentAmmo, ammoReserve, weaponData.magazineCapacity, weaponData.weaponName, isReloading, false);
    }

    public void PullTrigger()
    {
        if (isReloading || currentAmmo <= 0 || !isInHellWorld || isEquippingOrUnequipping) return;
        Shoot();
    }
    
    public void Reload()
    {
        if (ammoReserve <= 0 || currentAmmo >= weaponData.magazineCapacity || isEquippingOrUnequipping || isReloading || !isInHellWorld) return;
        reloading?.Invoke();
        StartCoroutine(CoroutineReload());
    }

    private void InstantReload()
    {
        reloading?.Invoke();
        ammoReserve -= 2;
        currentAmmo = weaponData.magazineCapacity;
        UpdateAmmo(true);
        //updateCrystalInWeapon?.Invoke(currentAmmo, ammoReserve);
        enableCrystal?.Invoke(true);
    }

    public IEnumerator CoroutineReload()
    {
        isReloading = true;
        animator.ChangeAnimation("Reload", 0.0f, 0.0f);
        PlayReloadSound();
        ammoReserve -= 2;
        currentAmmo = weaponData.magazineCapacity;
        UpdateAmmo(true);
        updateCrystalInWeapon?.Invoke(currentAmmo, ammoReserve);
        yield return new WaitForSeconds(weaponData.reloadTime);
        isReloading = false;
        
        if (playHeavenAnimationAfterReloading && !isInHellWorld)
        {
            animator.ChangeAnimation("SwitchToHeavenWorld", 0.5f);
            playHeavenAnimationAfterReloading = false;
        }

    }

    public void PlayReloadSound()
    {
        ////SoundManager.PlaySoundWithDelay(SoundType.CLIPOUT ,0.5f, 0f);
        //SoundManager.PlaySoundWithDelay(SoundType.COVERUP, 0.5f, 0.25f);
        //SoundManager.PlaySoundWithDelay(SoundType.CLIPIN, 0.3f, 0.58f);
        //SoundManager.PlaySoundWithDelay(SoundType.COVERDOWN, 0.5f, 0.47f);
    }

    public void OnPlayReloadSound(int index)
    {
        if (index == 1)
        {
            SoundManager.PlaySoundWithDelay(SoundType.COVERUP, 0.5f, 0f);
        }
        else if (index == 2)
        {
            SoundManager.PlaySoundWithDelay(SoundType.CLIPIN, 0.5f, 0f);
        }
        else if(index == 3)
        {
            SoundManager.PlaySoundWithDelay(SoundType.COVERDOWN, 0.5f, 0f);
        }
        else if (index == 4)
        {
            SoundManager.PlaySoundWithDelay(SoundType.CLIPOUT, 0.5f, 0f);
        }
    }
    
    public void AddAmmoAmount(int amount)
    {
        ammoReserve += amount;
        UpdateAmmo(false);
        if (currentAmmo == 0)
        {
            InstantReload();
        }
    }

    public void DisableGun()
    {
        
    }

    public void OnSwitchWorld(bool isInHellWorld)
    {
        if(inStartAnimation) return;

        this.isInHellWorld = isInHellWorld;
        
        if (isInHellWorld && !isReloading) {SoundManager.PlaySound(SoundType.DRAW);}
        
        if (isReloading && !isInHellWorld)
        {
            playHeavenAnimationAfterReloading = true;
        }
        else if (!isReloading)
        {
            if (animator != null)
            {
                animator.ForcePlay(isInHellWorld ? "SwitchToHellWorld" : "SwitchToHeavenWorld");
            }
            isEquippingOrUnequipping = true;
            currentEquipTime = weaponData.equipTime;
        }
    }

    private void OnTriggerStartAnimation()
    {
        // trigger animation
        gunModel.SetActive(false);
        StartCoroutine(TriggerStartAnimationWithDelay());
        enableCrystal?.Invoke(false);
        inStartAnimation = true;
    }

    private IEnumerator TriggerStartAnimationWithDelay()
    {
        yield return new WaitForSeconds(0.1f);
        gunModel.SetActive(true);
        animator.ForcePlay("GameStart");
    }

    // triggered by an animation event in the start anim
    public void OnEnableCrystalInStartAnim()
    {
        enableCrystal?.Invoke(true);
    }

    public void OnStartAnimationFinished()
    {
        startAnimationFinished?.Invoke();
        inStartAnimation = false;
        OnSwitchWorld(isInHellWorld);
    }
    
    private void UpdateAmmo(bool updateCurrentCrystalImage)
    {
        currentAmmo = Mathf.Clamp(currentAmmo, 0, weaponData.magazineCapacity);
        ammoReserve = Mathf.Clamp(ammoReserve, 0, 9999);
        updateAmmo?.Invoke(currentAmmo, ammoReserve, weaponData.magazineCapacity, weaponData.weaponName, isReloading, updateCurrentCrystalImage);
    }
}
