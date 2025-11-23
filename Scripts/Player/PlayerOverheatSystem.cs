using System;
using UnityEngine;

public class PlayerOverheatSystem : MonoBehaviour
{
    
    // Serialize Fields
    [SerializeField] private bool overheatSystemEnabled = true;
    [SerializeField] private float maxDurationInHell = 3f;
    [SerializeField] private float overheatDuration = 3f;
    [Space] 
    [SerializeField] private float reductionRate = 0.7f;
    [SerializeField] private float increaseRate = 0.7f;
    [Space]
    [SerializeField] private float switchDelay = 0.5f;
    
    
    // Variables
    private float currentSwitchDelay;
    private bool isSwitching;

    private float currentOverheatDuration;
    private bool isOverheated;
    private bool overheatedJustStarted;
    private bool isInHell;
    
    public Action onOverheated;
    public static Action<float, float, bool> onOverheatInfoChanged;
    public bool IsOverheated => isOverheated;
    
    public void Start()
    {
        currentOverheatDuration = 0f;
    }
    
    public void UpdateOverheatSystem(WorldSwitcherState state)
    {
        if (state == WorldSwitcherState.Hell)
        {
            isInHell = true;
            if (!overheatSystemEnabled || isOverheated) return;
            currentOverheatDuration += Time.deltaTime * increaseRate;
            if (currentOverheatDuration >= maxDurationInHell && !isOverheated)
            {
                isOverheated = true;
                overheatedJustStarted = true;
                isInHell = false;
                onOverheated?.Invoke();
            }
        }
        else if (state == WorldSwitcherState.Normal)
        {
            isInHell = false;
            if (!overheatSystemEnabled) return;
            currentOverheatDuration -= Time.deltaTime * reductionRate;
        }
        currentOverheatDuration = Mathf.Clamp(currentOverheatDuration, 0, maxDurationInHell);
        onOverheatInfoChanged?.Invoke(currentOverheatDuration, maxDurationInHell, isOverheated);
    }
    
    
    public void CheckIfOverheated()
    {
        if (isOverheated)
        {
            if (overheatedJustStarted)
            {
                overheatedJustStarted = false;
                currentOverheatDuration = overheatDuration;
            }
            
            currentOverheatDuration -= Time.deltaTime;
            
            if (currentOverheatDuration  <= 0)
            {
                isOverheated = false;
                overheatedJustStarted = true;
            }
        }
    }

    public void CoolOverheatBar(float amount)
    {
        currentOverheatDuration -= amount;
    }
    
}