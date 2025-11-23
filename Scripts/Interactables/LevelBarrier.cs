using UnityEngine;

public class LevelBarrier : Interactable
{
    public override void Trigger()
    {
        base.Trigger();
        Destroy(gameObject);
    }
}
