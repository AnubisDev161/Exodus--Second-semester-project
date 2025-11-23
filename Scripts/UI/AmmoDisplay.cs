using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private List<CrystalIcon> ammoReserveIcons;

    private string weaponName;
    private int gunCurrentAmmo;
    private bool isReloading;
    private int ammoReserve;
    private int magazineCapacity;

    void Start()
    {
        Subject.updateAmmo += OnUpdateAmmo;
        foreach (var ammo in ammoReserveIcons)
        {
            ammo.SetImageToCracked(false);
            return;
        }
    }
    private void OnUpdateAmmo(int currentAmmo, int ammoReserve, int magazineCapacity, string gunName, bool isReloading, bool updateCurrentCrystalImage)
    {
        gunCurrentAmmo = currentAmmo;
        this.ammoReserve = ammoReserve;
        weaponName = gunName;
        this.isReloading = isReloading;
        this.magazineCapacity = magazineCapacity;
        UpdateAmmoCounter(updateCurrentCrystalImage);
        
    }
 
    private void UpdateAmmoCounter(bool updateCurrentCrystalImage)
    {
        var totalAmmoCount = (ammoReserve + gunCurrentAmmo);
        var adjustmentValue = 0;
        if(totalAmmoCount % 2 != 0)
        {
            adjustmentValue = 1;
        }

        var ammoIconCount = (totalAmmoCount + adjustmentValue) / 2;

        if (ammoIconCount > ammoReserveIcons.Count)
        {
            ammoIconCount = ammoReserveIcons.Count;
        }

        //// reset all icons
        foreach (var ammoIcon in ammoReserveIcons)
        {
            if (ammoIcon == null) return;
        
            ammoIcon.image.enabled = false;
            
        }

        // enable as many icons as there is ammo left 
        for (var i = 0; i < ammoIconCount; i++)
        {
            var crystal = ammoReserveIcons[i].GetComponent<CrystalIcon>();

            if (crystal == null) return;

            crystal.image.enabled = true;

        }

        if (totalAmmoCount % 2 != 0)
        {
            ammoReserveIcons[0].SetImageToCracked(true);
        }
        else
        {
            ammoReserveIcons[0].SetImageToCracked(false);
        }
    }
}
