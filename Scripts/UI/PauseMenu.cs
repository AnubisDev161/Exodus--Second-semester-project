using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class PauseMenu : MonoBehaviour
{
    private Canvas canvas;
    private bool isPaused = false;
    private bool exitIsPressed = false;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;
    
    private void Start()
    {
        canvas = GetComponent<Canvas>();
        Resume();
    }
    
    private void Update()
    {
        if (PlayerInputController.Instance.OpenPauseMenu.WasPressedThisFrame() && isPaused)
        {
            Resume();
        }
        else if (PlayerInputController.Instance.OpenPauseMenu.WasPressedThisFrame() && !isPaused)
        {
            Pause();
        }
    }

    public void Pause()
    {
        hud.GetComponent<Canvas>().enabled = false;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canvas.enabled = true;
        Time.timeScale = 0;
        SoundManager.PlaySound(SoundType.UI_OPEN);
        MusicManager.PauseMusic();
    }

    public void Resume()
    {
        if (exitIsPressed) return;
        hud.GetComponent<Canvas>().enabled = true;
        CloseSettings();
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canvas.enabled = false;
        Time.timeScale = 1;
        MusicManager.ResumeMusic();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneController.sceneController.SwitchSceneByIndex(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenSettings()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
    
    public void ReturnToMainMenu()
    {
        exitIsPressed = true;
        Time.timeScale = 1;
        SceneController.sceneController.SwitchSceneByName("MainMenu");
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
