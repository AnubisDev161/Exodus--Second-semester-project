using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private Color gizmoColor = Color.white;
    [SerializeField] private float radius = 1.0f;
    [SerializeField] private List<SpawnPoint> spawnPoints;
    [SerializeField] private float spawnDelay = 0;
    [SerializeField] bool spawnContinuously;
    [SerializeField] bool spawnOnAwake;

    public bool wasTriggered { get; private set; }
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void Awake()
    {
        if(spawnOnAwake)
        {
            Trigger();
        }
    }

    public void Trigger()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points assigned to spawn trigger!");
            return;
        }
        StartCoroutine(StartSpawnTimer());
    }

    private void TriggerRandomSpawnPoint()
    {
        var randomIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
        spawnPoints[randomIndex].SpawnEnemies();
    }

    IEnumerator StartSpawnTimer()
    {
        wasTriggered = true;   
        yield return new WaitForSeconds(spawnDelay);
        TriggerRandomSpawnPoint();
        if(spawnContinuously)
        {
            wasTriggered = false;
        }
    }

    public void Stop()
    {
        gameObject.SetActive(false);    
    }
}
