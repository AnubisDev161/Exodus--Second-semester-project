using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using System;

public class SaveSurvivorsMission : MissionBase
{
    public static Action questFailed;
    private int currentSurvivors;
    private int maxSurvivors;
    private int howManyNeedToBeAlive;

    private void OnEnable()
    {
        ConvoyAndEnemyNotifier.npcDied += UpdateMission;
    }

    private void OnDisable()
    {
        ConvoyAndEnemyNotifier.npcDied -= UpdateMission;
    }

    private void Start()
    {
        SetupMission();

        if (levelGoal == null)
        {
            Debug.LogWarning("No level goal is assigned to mission!");
            return;
        }
    }
    
    public override void SetupMission()
    {
        maxSurvivors = ConvoyAndEnemyNotifier.instance.GetListLength();
        currentSurvivors = ConvoyAndEnemyNotifier.instance.GetListLength();
        howManyNeedToBeAlive = Mathf.CeilToInt(maxSurvivors / 3f);
       // missionText.text += $" \n At least {howManyNeedToBeAlive} of them need to Survive.";
        UpdateMission();
        levelGoal?.Initialize(howManyNeedToBeAlive);
    }

    public override void UpdateMission()
    {
        currentSurvivors = Mathf.Clamp(currentSurvivors, 0, maxSurvivors);
        currentSurvivors = ConvoyAndEnemyNotifier.instance.GetListLength();
        //if (!MissionCompleted)
        //{
        //    progressText.text = $"{currentSurvivors} / {maxSurvivors}";
        //}
        
        if (currentSurvivors < howManyNeedToBeAlive)
        {
            questFailed?.Invoke(); // connected with death screen 
        }
    }
  
    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(3f);
        missionText.text = "";
        progressText.text = "";
    }
}

