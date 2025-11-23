using UnityEngine;


public class GunWorldSwitchHandler : MonoBehaviour, IWorldSwitchListener
{
    [SerializeField] private GunScript gun;
    private void OnEnable()
    {
        WorldSwitcher.switchWorld += OnSwitchWorld;
    }

    private void OnDisable()
    {
        WorldSwitcher.switchWorld -= OnSwitchWorld;
    }

    public void OnSwitchWorld(bool isInHellWorld)
    {
        if(gun == null) return;

        if(isInHellWorld)
        {
            gun.gameObject.SetActive(true); // is disabled via an animation event but needs to be enabled from outside
        }
    }
}
