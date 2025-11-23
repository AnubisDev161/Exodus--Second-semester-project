using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;

public class Crystal : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private AudioSource crackingSound;
    [SerializeField] private AudioSource shatteringSound;
    // [SerializeField] private Material normalMaterial;
    // [SerializeField] private Material crackedMaterial;
    [SerializeField] MeshRenderer normalCrystal;
    [SerializeField] MeshRenderer crackedCrystal;

    [SerializeField] private Light pointLight;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color crackedColor;
    [SerializeField] private float crackedLightIntensity;
    [SerializeField] private float normalLightIntensity;
    [SerializeField] private float normalRange;
    [SerializeField] private float crackedRange;
     // particles
    [SerializeField] private Vector2 crackedParticleEmissionAmount;
    [SerializeField] private Vector2 shatteredParticleEmissionAmount;
    [SerializeField] private Vector2 crackedParticleSize;
    [SerializeField] private Vector2 shatteredParticleSize;

    private MeshRenderer currentCrystalObject;

    private void Awake()
    {
        currentCrystalObject = normalCrystal;
    }

    public void OnEnableCrystalModel(bool enable)
    {
        Enable(enable);
    }

    private void Enable(bool enable)
    {
        currentCrystalObject.enabled = enable;
        pointLight.enabled = enable;
    }

    private void OnEnable()
    {
        GunScript.updateCrystalInWeapon += OnUpdateImage;
       
    }

    private void OnDisable()
    {
        GunScript.updateCrystalInWeapon -= OnUpdateImage;
    }

    public void OnUpdateImage(int currentAmmo, int ammoReserve)
    {
        var totalAmmo = ammoReserve + currentAmmo;

        if (totalAmmo % 2 == 0)
        {
            SoundManager.PlaySound(SoundType.CRYSTAL_SHATTERING);

            crackedCrystal.enabled = false;
            currentCrystalObject = normalCrystal;
           
            pointLight.intensity = normalLightIntensity;
            pointLight.color = normalColor;
            pointLight.range = normalRange;
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            var emission = particleSystem.emission;
            emission.SetBursts(new ParticleSystem.Burst[] {
            new ParticleSystem.Burst(0, shatteredParticleEmissionAmount.y)
            });
            particleSystem.Play();
        }
        else
        {
            SoundManager.PlaySound(SoundType.CRYSTAL_CRACKING);
            
            normalCrystal.enabled = false;
            currentCrystalObject = crackedCrystal;
           
            pointLight.intensity = crackedLightIntensity;
            pointLight.color = crackedColor;
            pointLight.range = crackedRange;
            var oldPosition = particleSystem.transform.position;

            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            var emission = particleSystem.emission;
            emission.SetBursts(new ParticleSystem.Burst[] {
             new ParticleSystem.Burst(0, crackedParticleEmissionAmount.y)
            });
            particleSystem.Play();
        }

        if (totalAmmo == 0)
        {
            Enable(false);
        }
        else
        {
            Enable(true);
        }
        // GetComponentInChildren<CrystalMeshSwitcher>().SwitchMesh();
    }
}
