using UnityEngine;
using UnityEngine.Events;

public class POITrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent triggered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            triggered?.Invoke();
        }
    }
}
