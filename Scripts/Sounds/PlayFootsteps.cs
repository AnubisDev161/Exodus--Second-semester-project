using UnityEngine;

public class PlayFootsteps : MonoBehaviour
{

    public void PlaySound()
    {
        SoundManager.PlaySound(SoundType.FOOTSTEPS);
    }
}
