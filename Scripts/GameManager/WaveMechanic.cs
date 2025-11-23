using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WaveMechanic : MonoBehaviour
{
    
    
    [SerializeField] private float randomness = 1.0f;
    [SerializeField] private SpawnPoint[] spawnPoints;
    [SerializeField] private float maxOfEnemies;
    [SerializeField] private int remainingEnemiesToKill;
    [SerializeField] private LevelGoal levelGoal;
    
    private int currentEnemiesAlive;
    private int enemiesKilled;
    private bool looping = true;
    private int lastEnemiesAlive;
    private bool stopSpawning = false;
    public static Action<int> onUpdateMission;
    bool thanksForPlayingSended;
    
    void OnEnable()
    {
        Enemy.OnEnemyDied += OnEnemyKilled;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDied -= OnEnemyKilled;
    }
    
    private void TriggerRandomSpawnPoint()
    {
        StartCoroutine(EnemyWaveLoop());
    }

    private IEnumerator EnemyWaveLoop()
    {
        while (looping)
        {
            currentEnemiesAlive = CheckCurrentEnemiesAlive();
            
            if (spawnPoints.Length > 0 && currentEnemiesAlive <= maxOfEnemies && !stopSpawning)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                spawnPoints[spawnIndex].SpawnEnemies();
            }
            float waitTime = Random.Range(0f, randomness);
            
            if (enemiesKilled >= remainingEnemiesToKill && CheckCurrentEnemiesAlive() == 0 && !thanksForPlayingSended)
            {
                levelGoal.PlayThanksForPlaying();
                thanksForPlayingSended = true;
                looping = false;
            }
            else if (enemiesKilled >= remainingEnemiesToKill)
            {
                stopSpawning = true;
            }
            
            
            
            yield return new WaitForSeconds(randomness);
        }
        
    }

    private void OnEnemyKilled()
    {
        enemiesKilled++;
        remainingEnemiesToKill--;
        onUpdateMission?.Invoke(remainingEnemiesToKill);
    }
    


    private void Start()
    {
        TriggerRandomSpawnPoint();
        onUpdateMission?.Invoke(remainingEnemiesToKill);
    }

    private int CheckCurrentEnemiesAlive()
    {
        int amount = 0;
        foreach(SpawnPoint spawnPoint in spawnPoints)
        {
            foreach(Transform child in spawnPoint.transform)
            {
                if (child.GetComponent<Enemy>() != null)
                {
                    amount++;
                }
            }
        }
        return amount;
    }
    
}
