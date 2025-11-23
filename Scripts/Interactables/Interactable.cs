using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(BoxCollider))]
public class Interactable : MonoBehaviour
{
    [SerializeField] protected UnityEvent triggered;
    protected bool wasTriggered;
    public virtual void Trigger()
    {
        if (!wasTriggered)
        {
            Debug.Log("Lever triggered!");
            triggered.Invoke();
            wasTriggered = true;
        }
    }
}
