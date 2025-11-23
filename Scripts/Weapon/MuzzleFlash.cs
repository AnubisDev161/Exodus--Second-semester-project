using UnityEngine;


public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystemPrefab;

    public void OnShoot()
    {
        if (particleSystemPrefab != null)
        {
            Instantiate(particleSystemPrefab, transform);
           

        }

    }

}
