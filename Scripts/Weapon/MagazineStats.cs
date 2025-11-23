using UnityEngine;

[System.Serializable]
public class MagazineStats 
{
    // Stats
    [SerializeField, Min(0f), Tooltip("Reload Time in Seconds.")]                          public float reloadTime = 2f;
    [SerializeField, Min(0f), Tooltip("How much ammo the gun can shoot.")]                 public int magazineCapacity = 20;
    [SerializeField, Min(0f), Tooltip("Current ammo amount.")]                             public int currentAmmo = 20;
    [SerializeField, Min(0f), Tooltip("How much ammo he has in reserve")]                  public int ammoInReserve = 50;
    [SerializeField] public bool infiniteAmmo = false;

    // // Getter 
    // public float ReloadTime => reloadTime;
    // public int MagazineCapacity => magazineCapacity;
    // public int CurrentAmmo => currentAmmo;
    // public bool InfiniteAmmo => infiniteAmmo;
    // public int AmmoInReserve => ammoInReserve;

}