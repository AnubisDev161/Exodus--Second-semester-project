using UnityEngine;

public class ParticlesSwitcher : MonoBehaviour, IWorldSwitchListener
{
    [SerializeField] private ParticleSystem hellRunParticles;
    [SerializeField] private ParticleSystem hellStandingParticles;
    [SerializeField] private ParticleSystem heavenRunParticles;
    [SerializeField] private ParticleSystem heavenWalkParticles;

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
        var standingMain = hellStandingParticles.main;
        Color newStandingColor = hellStandingParticles.main.startColor.color;  
        var runningMain = hellRunParticles.main;
        Color newRunningColor = hellRunParticles.main.startColor.color;   
        if (isInHellWorld)
        {
           /// hellRunParticles.main.startColor.color = new Color(0, 0, 0, 0);
            newStandingColor.a = 1;  // Modify the alpha value
            standingMain.startColor = newStandingColor;
            newRunningColor.a = 1;
            runningMain.startColor = newRunningColor;
        }
        else
        {
            newStandingColor.a = 0;  // Modify the alpha value
            standingMain.startColor = newStandingColor;
            newRunningColor.a = 0;
            runningMain.startColor = newRunningColor;
        }
    }
}
