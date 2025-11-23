using UnityEngine;

public class PlayerNpcHealthBarViewRange : MonoBehaviour
{
    // if an npc enters the player'S view range, it will display its health bar.
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ConvoyNPC>(out ConvoyNPC npc))
        {
            npc.OnNpcTriggeredInteractableRange(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ConvoyNPC>(out ConvoyNPC npc))
        {
            npc.OnNpcTriggeredInteractableRange(false);
        }
    }
}
