using UnityEngine;

public class LightSwitcher : MonoBehaviour, IWorldSwitchListener
{
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color hellColor;
    [SerializeField] private Light light;

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
        if(isInHellWorld)
        {
            light.color = hellColor;
        }
        else
        {
            light.color = defaultColor;
        }
    }
}
