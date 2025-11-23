using System.Collections;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class EndFightPopUpDisplay : MonoBehaviour, IWorldSwitchListener
{
    [SerializeField] private TutorialPopUpPanel panel;
    AudioSource endNarratorTape;
    [SerializeField] private PlayerInputController playerInput;
    [SerializeField] private WeaponAnimatonController weaponAnimationController;
    [SerializeField] AudioSource playerReaction;
    [SerializeField] private float playerReactionAnimationDelay;

    private bool inHell;

    private void Awake()
    {
        endNarratorTape = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        WorldSwitcher.switchWorld += OnSwitchWorld;
    }

    private void OnDisable()
    {
         WorldSwitcher.switchWorld -= OnSwitchWorld;
    }

    public void OnShowTutorialPopUp(bool show)
    {
        playerInput.StartCoroutine(playerInput.DisablePlayerActionMapAfterDelay());
        panel.SlideIn(show, 0);
        endNarratorTape.Play();

        if (inHell)
        {
            GunWorldSwitchTrigger.forceWorldSwtich();
        }
        weaponAnimationController.ForcePlay("Idle");
        StartCoroutine(PlayEndAnimation(endNarratorTape.clip.length));
    }
    
    private IEnumerator PlayEndAnimation(float animationDelay)
    {
        yield return new WaitForSeconds(animationDelay);
        weaponAnimationController.ChangeAnimation("End", 0, playerReactionAnimationDelay);
        playerReaction.Play();
    }

    public void OnSwitchWorld(bool isInHellWorld)
    {
        inHell = isInHellWorld;
    }
}
