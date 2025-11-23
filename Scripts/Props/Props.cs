using Unity.VisualScripting;
using UnityEngine;

public class Props : MonoBehaviour, IWorldSwitchListener
{
    [SerializeField] GameObject defaultWorld;
    [SerializeField] GameObject hellWorld;

    public void OnSwitchWorld(bool isInHellWorld)
    {
        if (isInHellWorld)
        {
            defaultWorld.SetActive(false);
            hellWorld.SetActive(true);
        }
        else
        {
            defaultWorld.SetActive(true);
            hellWorld.SetActive(false);
        }
    }

    private void OnEnable()
    {
        WorldSwitcher.switchWorld += OnSwitchWorld;
    }

    private void OnDisable()
    {
        WorldSwitcher.switchWorld -= OnSwitchWorld;
    }
}
