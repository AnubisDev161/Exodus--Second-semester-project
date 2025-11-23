using System;
using UnityEngine;

public class WeaponHand : MonoBehaviour
{
    [SerializeField] private GunScript playersWeapon;
    private static bool playerHoldingGun = false;

    private void Start()
    {
        if (playersWeapon == null)
        {
            playerHoldingGun = false;
            Debug.Log("Player has no weapon!");
        }
        else
        {
            playerHoldingGun = true;
            Debug.Log($"Player is holding {playersWeapon.WeaponName}");
        }
        
    }

    public void CollectAmmo(int ammo)
    {
        playersWeapon.AddAmmoAmount(ammo);
    }
    
    void Update()
    {
        if (playerHoldingGun)
        {
            TryShoot();
            TryReload();
        }
    }

    private void TryShoot()
    {
        if (PlayerInputController.Instance.ShootAction.IsPressed() && playersWeapon.GetGunType == GunType.Automatic)
        {
            // TODO clean this code when you have time
            

            PullTheTrigger();
        }
        else if (PlayerInputController.Instance.ShootAction.WasPressedThisFrame() && playersWeapon.GetGunType == GunType.Semi)
        {
            PullTheTrigger();
        }
        else
        {
            return;
        }

    }

    private void PullTheTrigger()
    {
        playersWeapon.PullTrigger();
    }

    private void TryReload()
    {
        if (PlayerInputController.Instance.ReloadAction.WasPressedThisFrame() && !playersWeapon.IsReloading)
        {
            playersWeapon.Reload();
        }
    }

    public bool TryCollectAmmo(int additionalAmmo)
    {
        if(playersWeapon.ammoReserve + playersWeapon.CurrentAmmo < playersWeapon.MaxAmmo -1)
        {
            CollectAmmo(additionalAmmo);
            return true;
        }
        return false;
    }
}
