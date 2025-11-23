using UnityEngine;
using UnityEngine.Events;

public class Demon : MonoBehaviour
{
    [SerializeField] UnityEvent<IDetectable> AttackRangeEntered;
 
    public void OnTargetDetected(IDetectable target)
    {
        AttackRangeEntered?.Invoke(target);
        if (target != null)
        {
            
        }
    }

}
