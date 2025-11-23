using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialHandler : MonoBehaviour
{
    public static Action triggerStartAnimation;
    public static Action<bool> enablePlayerInputMap;
    public static Action<bool> enableHUD;
    public static Action enableWeaponDissolve;
    public static Action<bool> enableTutorialInputMap;

    bool playStartAnimationInThisScene;

    private void OnEnable()
    {
        GunScript.startAnimationFinished += OnPlayerStartingAnimationFinished;
        Tutorial.tutorialFinished += OnTutorialVideoFinished;
        Tutorial.tutorialStarted += OnTutorialVideoStarted;
        SceneManager.sceneLoaded += OnSceneLoaded;
        // player clicks left mouse button
    }

    private void OnDisable()
    {
        GunScript.startAnimationFinished -= OnPlayerStartingAnimationFinished;
        Tutorial.tutorialFinished -= OnTutorialVideoFinished;
        Tutorial.tutorialStarted -= OnTutorialVideoStarted;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(playStartAnimationInThisScene)
        {
            enableHUD?.Invoke(false);
            enablePlayerInputMap?.Invoke(false);
            enableTutorialInputMap?.Invoke(true);
            triggerStartAnimation?.Invoke();
            enableWeaponDissolve?.Invoke();
        }
    }

    private void OnTutorialVideoFinished()
    {
        playStartAnimationInThisScene = true;
    }

    public void OnPlayerStartingAnimationFinished()
    {
        enableHUD?.Invoke(true);
        enablePlayerInputMap?.Invoke(true); // player should be enabled after special animation is finished
        playStartAnimationInThisScene = false;
    }

    public void OnTutorialVideoStarted()
    {
        enableHUD?.Invoke(false);
        enablePlayerInputMap?.Invoke(false);
        enableTutorialInputMap?.Invoke(true);
    }
}