using UnityEngine;

[System.Serializable]
public class WeaponStats
{
    // Stats
    [SerializeField] public string weaponName = "Negev";
    [SerializeField] public GunType gunType;
    [SerializeField, Range(0f, 10), Tooltip("How accurate the gun will be on the x axis.")]              public float maxXSpread = 3.5f;
    [SerializeField, Range(0f, 10), Tooltip("How accurate the gun will be on the y axis.")]              public float maxYSpread = 3.5f;
    [SerializeField, Min(0f), Tooltip("Cooldown between shots in seconds.")]                             public float fireRate = 0.1f;

    
    // Getter 
    public string WeaponName => weaponName;
    public GunType GunType => gunType;
    public float MaxXSpread => maxXSpread;
    public float MaxYSpread => maxYSpread;
    public float FireRate => fireRate;

}

public enum GunType
{
    Semi,
    Automatic
}