using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeSwitcher : MonoBehaviour, IWorldSwitchListener
{
    private Volume volume;
    private Animation hellWorldAnimation;
    [SerializeField] private VolumeProfile heavenVolumeProfile;
    [SerializeField] private VolumeProfile hellVolumeProfile;

    private void Awake()
    {
        volume = GetComponent<Volume>();
        hellWorldAnimation = GetComponent<Animation>();

    }
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
        if (hellWorldAnimation != null)
        {
            hellWorldAnimation.enabled = isInHellWorld;
        }

        if(isInHellWorld)
        {
            volume.profile = hellVolumeProfile;
        }
        else
        {
            volume.profile = heavenVolumeProfile;
        }
    }
}
