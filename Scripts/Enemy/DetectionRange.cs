using UnityEngine;
using UnityEngine.Events;

public class DetectionRange : MonoBehaviour
{
    public UnityEvent<IDetectable> detectionRangeTriggered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDetectable>(out IDetectable detectableComponent))
        {
            detectionRangeTriggered?.Invoke(detectableComponent);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IDetectable>(out IDetectable detectableComponent))
        {
            detectionRangeTriggered?.Invoke(null);
        }
    }
}
