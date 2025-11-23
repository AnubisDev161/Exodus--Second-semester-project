using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class RaycastBullet : MonoBehaviour
{
    [SerializeField] GameObject bulletTrail;
    [SerializeField] ParticleSystem impactParticles;
    [SerializeField] LayerMask detectionMask;

    [SerializeField] float trailLifeTime;
    [SerializeField] float impactStrength = 200;
    [SerializeField] float shootDistance = 2;
   
    private int bulletSpeed = 10;

    public int BulletSpeed
    {
        get { return bulletSpeed; }
        set { bulletSpeed = value; }
    }


    public void Fire(float xSpread, float ySpread, LayerMask hitDetectionMask, int damage)
    {
        // get the middle of the screen
        Ray centerRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 shootOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Vector3 shootDirection = centerRay.direction;

        // shoot ray
        if (Physics.Raycast(shootOrigin, shootDirection, out RaycastHit hit, shootDistance, detectionMask + hitDetectionMask))
        {
            if (hit.collider.TryGetComponent<IHitable>(out IHitable hitableComponent))
            {
                SoundManager.PlaySound(SoundType.BULLET_IMPACT_FLESH);
                hitableComponent.OnHit(damage, shootDirection * impactStrength);
            }
            else
            {
                SoundManager.PlaySound(SoundType.BULLET_IMPACT_WALL);
            }
        }

        StartCoroutine(StartDestructionTimer());
    }

    private IEnumerator StartDestructionTimer()
    {
        yield return new WaitForSeconds(trailLifeTime);
        Destroy(gameObject);
    }
    private IEnumerator SpawnTrail(TrailRenderer trailRenderer, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = trailRenderer.transform.position;

        while (time < trailLifeTime)
        {
            trailRenderer.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trailRenderer.time;

            yield return null;
        }

        trailRenderer.transform.position = hit.point;
        Instantiate(impactParticles, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(trailRenderer.gameObject, trailRenderer.time);
        Destroy(gameObject);
    }

    private IEnumerator SpawnTrail(TrailRenderer trailRenderer, Vector3 impactPoint)
    {
        float time = 0;
      
        Vector3 startPosition = trailRenderer.transform.position;

        while (time < trailLifeTime)
        {
            trailRenderer.transform.position = Vector3.Lerp(startPosition, impactPoint, time);
            time += Time.deltaTime / trailRenderer.time;

            yield return null;
        }
        
        trailRenderer.transform.position = impactPoint;

        Destroy(trailRenderer.gameObject, trailRenderer.time);
        Destroy(gameObject);
    }
}
