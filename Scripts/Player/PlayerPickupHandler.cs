using UnityEngine;

public class PlayerPickupHandler : MonoBehaviour, IPickupReceiver
{
    private WeaponHand weaponHand;
    private PlayerHealth playerHealth;
    private PlayerOverheatSystem playerOverheatSystem;

    private void Awake()
    {
        weaponHand = GetComponent<WeaponHand>();
        playerHealth = GetComponent<PlayerHealth>();
        playerOverheatSystem = GetComponent<PlayerOverheatSystem>();
    }

    public bool TryReceivePickup(PickupType type, int amount)
    {
        if (weaponHand == null || playerHealth == null || playerOverheatSystem == null) return false;
        switch (type)
        {
            case PickupType.Ammo:
                if(weaponHand.TryCollectAmmo(amount))
                {
                    SoundManager.PlaySound(SoundType.PICKUP);
                    return true;
                }
                break;
            case PickupType.Health:
                playerHealth?.UpdateHealth(amount);
                SoundManager.PlaySound(SoundType.PICKUP);
                break;
            case PickupType.Overheat:
                playerOverheatSystem.CoolOverheatBar(amount);
                SoundManager.PlaySound(SoundType.PICKUP);
                break;
            default:
                Debug.LogWarning($"Unhandled pickup type: {type}");
                break;
        }
        return false;
    }
    
}