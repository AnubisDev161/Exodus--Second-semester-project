using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupType pickupType;
    [SerializeField] private int amount;


    private void Awake()
    {
        Material material = GetComponent<Material>();
        if (material == null) return;
        switch (pickupType)
        {
            case PickupType.Ammo:
                break;
            case PickupType.Health:
                break;
            case PickupType.Overheat:
                break;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IPickupReceiver>(out IPickupReceiver pickupReceiver))
        {
            // check if receiver can collect
            if (pickupReceiver.TryReceivePickup(pickupType, amount))
            {
                Destroy(gameObject);
            }
        }
    }
}

public enum PickupType
{
    Ammo,
    Health,
    Overheat
}