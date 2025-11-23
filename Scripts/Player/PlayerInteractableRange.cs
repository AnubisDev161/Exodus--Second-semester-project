using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]
public class PlayerInteractableRange : MonoBehaviour
{
    [SerializeField] private Player player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Lever>(out Lever lever))
        {
            player.InteractableInRange(lever);
            lever.InFocus(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Lever>(out Lever lever))
        {
            player.InteractableInRange(null);
            lever.InFocus(false);
        }
    }
}
