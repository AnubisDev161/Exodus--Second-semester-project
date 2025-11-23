using UnityEngine;

[RequireComponent (typeof(Animation))]
[RequireComponent(typeof(AudioSource))]
public class Door : Interactable
{
    Animation openAnimation;
    AudioSource audioSource;

    private void Start()
    {
        openAnimation = GetComponent<Animation>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void Trigger()
    {
        openAnimation.Play();
        audioSource.Play();
    }

    public void OnOpenAnimationFinished()
    {
        base.Trigger();
       // Destroy(gameObject);

    }
}
