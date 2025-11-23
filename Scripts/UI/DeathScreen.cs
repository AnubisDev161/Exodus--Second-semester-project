using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private int menuSceneIndex;
    [SerializeField] private int gameSceneIndex;

    private Canvas canvas;
    public void LoadMenuScene()
    {
        SceneController.sceneController.SwitchSceneByName("MainMenu");
    }

    public void ReloadScene()
    {
        SceneController.sceneController.ReloadLastLevel();
    }

    private void OnEnable()
    {
        SaveSurvivorsMission.questFailed += SetTextToMissionFailed;
    }

    private void OnDisable()
    {
        SaveSurvivorsMission.questFailed -= SetTextToMissionFailed;
    }

    private void SetTextToMissionFailed()
    {
        ShowDeathScreen();
    }

    private void ShowDeathScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        canvas.enabled = true;
        // start smooth transition
        // game needs to be paused
    }

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SoundManager.PlaySound(SoundType.GAME_OVER);
    }
}
