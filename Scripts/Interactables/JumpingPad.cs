using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class JumpingPad : MonoBehaviour
{
    private Rigidbody rigidbody;

    [SerializeField] private float force;
    [SerializeField] private float cooldownTime;
    [SerializeField] private AudioSource jumpingPadTriggeredSound;

    private float currentCooldownTime;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        jumpingPadTriggeredSound.volume = SoundManager.GetSound(SoundType.JUMPING_PAD).volume;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player) && currentCooldownTime == 0)
        {
            player.JumpPadTriggerd(force);
            currentCooldownTime = cooldownTime;
            jumpingPadTriggeredSound.Play();
        }
    }

    private void Update()
    {
        if (currentCooldownTime > 0)
        {
            currentCooldownTime -= Time.deltaTime;
        }
        else
        {
            currentCooldownTime = 0;
        }
    }
}
