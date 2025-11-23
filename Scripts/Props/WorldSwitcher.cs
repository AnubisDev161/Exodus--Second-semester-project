using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class WorldSwitcher : MonoBehaviour
{
    [SerializeField] private WorldSwitcherState currentWorld;
    [SerializeField] private UnityEvent<bool> enableGun; 
    [SerializeField] private PlayerOverheatSystem playersOverheatSystem;

    
    public static Action<bool> switchWorld; // action
    private bool isInHell => currentWorld == WorldSwitcherState.Hell;
    private float currentSwitchDelay;
    private AudioSource heartBeatInHell;

    private void Awake()
    {
        heartBeatInHell = GetComponent<AudioSource>();
        heartBeatInHell.volume = SoundManager.GetSound(SoundType.HEART_BEAT).volume;
    }
    private void OnEnable()
    {
        GunWorldSwitchTrigger.forceWorldSwtich += OnForceWorldSwitch;
        HoverSwitch.forceWorldSwitch += OnForceWorldSwitch;
        if (playersOverheatSystem == null) return;
        playersOverheatSystem.onOverheated += OverheatTriggered;
    }

    private void OnDisable()
    {
        HoverSwitch.forceWorldSwitch -= OnForceWorldSwitch;
        GunWorldSwitchTrigger.forceWorldSwtich -= OnForceWorldSwitch;
        if (playersOverheatSystem == null) return;
        playersOverheatSystem.onOverheated -= OverheatTriggered;
    }

    private void SwitchWorld()
    {
        currentWorld = currentWorld == WorldSwitcherState.Hell ? WorldSwitcherState.Normal : WorldSwitcherState.Hell;
        switchWorld?.Invoke(isInHell); // Checks whether there are any listeners subscribed to the action

        if (isInHell)
        {
            SoundManager.PlaySound(SoundType.WORLD_SWITCH_TO_HELL);
            heartBeatInHell.Play();
        }
        else
        {
            SoundManager.PlaySound(SoundType.WORLD_SWITCH_TO_HEAVEN);
            heartBeatInHell.Stop();
        }
    }
    
    private void Update()
    {
        if (playersOverheatSystem == null) return;
        playersOverheatSystem.UpdateOverheatSystem(currentWorld);
        playersOverheatSystem.CheckIfOverheated();
        
        if (PlayerInputController.Instance.SwitchWorld.WasPressedThisFrame() && !playersOverheatSystem.IsOverheated)
        {
            SwitchWorld();
        }
    }

    private void OverheatTriggered()
    {
        if (currentWorld == WorldSwitcherState.Hell)
        {
            SwitchWorld();
        }
    }

    public void OnForceWorldSwitch()
    {
        SwitchWorld();
    }
}

public enum WorldSwitcherState
{
    Normal,
    Hell
}
