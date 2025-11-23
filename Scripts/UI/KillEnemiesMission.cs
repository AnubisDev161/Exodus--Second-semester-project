using System;
using UnityEngine;

public class KillEnemiesMission : MissionBase
{
    private int enemiesRemaining;
    private void Start()
    {
        SetupMission();
    }

    private void OnEnable()
    {
        WaveMechanic.onUpdateMission += UpdateEnemiesRemaining;
    }

    private void OnDisable()
    {
        WaveMechanic.onUpdateMission -= UpdateEnemiesRemaining;
    }
    
    public override void SetupMission()
    {
        missionText.text = "Kill all Enemies!";
        progressText.text = $"around {enemiesRemaining} of them are still remaining!";
    }
    
    public override void UpdateMission()
    {
        if (!MissionCompleted)
        {
            progressText.text = $"around {enemiesRemaining} of them are still remaining!";
        }
    }

    private void UpdateEnemiesRemaining(int remainingEnemies)
    {
        enemiesRemaining = remainingEnemies;
        if (enemiesRemaining > 0)
        {
            UpdateMission();
        }
        else
        {
            missionText.text = "";
            progressText.text = "Kill the rest of them!";
        }
        
   
    }
}
