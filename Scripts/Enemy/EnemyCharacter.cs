using UnityEngine;
using UnityEngine.Events;

public class EnemyCharacter : Character
{
    [SerializeField] UnityEvent<IDetectable> detectionRangeTriggered;
    [SerializeField] UnityEvent<IDetectable> AttackRangeTriggered;
    [SerializeField] UnityEvent disableDetectionZones;
 
    public void OnTargetDetectionRangeTriggered(IDetectable target)
    {
        detectionRangeTriggered?.Invoke(target);
    }

    public void OnTargetEnteredAttackRange(IDetectable target)
    {
        AttackRangeTriggered?.Invoke(target);
    }

    public void DisableDetection()
    {
        disableDetectionZones?.Invoke();
    }

    private void Start()
    {
        
    }
}
