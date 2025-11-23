using TMPro;
using UnityEngine;

public abstract class MissionBase : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI missionText;
    [SerializeField] protected TextMeshProUGUI progressText;
    [SerializeField] protected LevelGoal levelGoal;

    protected bool MissionCompleted = false;
    public abstract void SetupMission();
    public abstract void UpdateMission();
    
}
