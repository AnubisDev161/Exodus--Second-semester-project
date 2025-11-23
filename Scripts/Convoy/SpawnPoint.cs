using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPoint : MonoBehaviour, IWorldSwitchListener
{
    [SerializeField] private Color gizmoColor = Color.white;
    [SerializeField] [Range(1.5f, 10f)] private float radius = 1.0f;
    [SerializeField] private Enemy enemyPrefab;
    bool isInHellWorld;
    [SerializeField] private int minSpawnAmount = 1;
    [SerializeField] private int maxSpawnAmount = 1;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void OnEnable()
    {
        WorldSwitcher.switchWorld += OnSwitchWorld;
    }

    private void OnDisable()
    {
        WorldSwitcher.switchWorld -= OnSwitchWorld;
    }

    public void SpawnEnemies()
    {
        int randomAmount = Random.Range(minSpawnAmount, maxSpawnAmount);
        if(randomAmount == 1)
        {
            var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity, gameObject.transform);
            enemy.GetComponent<Enemy>().OnSwitchWorld(isInHellWorld);
            return;
        }
        for (int i = 0; i < randomAmount; i++)
        {
            var randomCirclePosition = Random.insideUnitCircle.normalized * radius;
            var randomSpawnPosition = new Vector3(randomCirclePosition.x, transform.position.y, randomCirclePosition.y);
            var enemy = Instantiate(enemyPrefab, transform.position + randomSpawnPosition, Quaternion.identity, gameObject.transform);
            enemy.GetComponent<Enemy>().OnSwitchWorld(isInHellWorld);
        }
    }
    

    public void OnSwitchWorld(bool isInHellWorld)
    {
        this.isInHellWorld = isInHellWorld;
    }
}
