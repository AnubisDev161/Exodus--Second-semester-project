using UnityEngine;

public interface IPickupReceiver
{
    bool TryReceivePickup(PickupType type, int amount);
}
