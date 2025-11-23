using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private SoundType currentSoundType;

    [SerializeField] private bool playSoundOnlyInHell = true;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        WorldSwitcher.switchWorld += OnSwitchWorld;
    }

    private void OnDisable()
    {
        WorldSwitcher.switchWorld -= OnSwitchWorld;
    }

    private void OnSwitchWorld(bool isInHell)
    {                                                                               
        if (audioSource == null || currentSoundType == SoundType.ENEMY_DEATH) return; // if an enemy dies, the death sounds needs to be heard for hit feedback
        
  
        if (!isInHell && playSoundOnlyInHell)
        {
            audioSource.volume = 0.0f;
        }
        else
        {
            audioSource.volume = 1.0f;
        }
        
    }
    
    public void PlaySound(SoundType soundType, float volume = 1.0f, float delay = 0.0f)
    {
        currentSoundType = soundType;
        StartCoroutine(PlaySoundWithDelay(soundType, volume, delay));
    }


    private IEnumerator PlaySoundWithDelay(SoundType soundType, float volume, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(SoundManager.GetSound(soundType).Sounds[0], volume);
    }
    
    public void PlaySoundFromEvent(string soundTypeName)
    {
        string[] parts = soundTypeName.Split(',');

        if (parts.Length >= 1 && Enum.TryParse(parts[0], out SoundType soundType))
        {
            var soundVolume = SoundManager.GetSound(soundType).volume;
            PlaySound(soundType, soundVolume);
        }
        else
        {
            Debug.LogWarning("SoundType: " + soundTypeName + " doesn't exist!");
        }
        
    }
}
