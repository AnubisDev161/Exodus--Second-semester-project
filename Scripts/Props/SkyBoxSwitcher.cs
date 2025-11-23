using UnityEngine;

public class SkyBoxSwitcher : MonoBehaviour, IWorldSwitchListener
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material hellMaterial;

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
        if (isInHellWorld)
        {
            RenderSettings.skybox = hellMaterial;
        }
        else
        {
            RenderSettings.skybox = defaultMaterial;
        }
    }
}
