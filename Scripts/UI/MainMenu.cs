using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject credits;
    [SerializeField] private Image resumeButtonImage;
    public static bool tabOpened = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if(SceneController.sceneController.HasGameAlreadyStarted())
        {
            EnableResumeButton();
        }
    }

    public void OnNewGamePressed()
    {
        SceneController.sceneController.SwitchSceneByIndex(2);
    }

    public void OnResumePressed()
    {
        SceneController.sceneController.ReloadLastLevel();
    }

    public void OnExitPressed()
    {
        
    
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OpenSettings()
    {
        mainScreen.SetActive(false);
        settings.SetActive(true);
        tabOpened = true;
    }

    public void CloseSettings()
    {
        mainScreen.SetActive(true);
        settings.SetActive(false);
        tabOpened = false;
    }

    public void OpenCredits()
    {
        mainScreen.SetActive(false);
        credits.SetActive(true);
        tabOpened = true;
    }

    public void CloseCredits()
    {
        mainScreen.SetActive(true);
        credits.SetActive(false);
        tabOpened = false;
    }
    
    private void EnableResumeButton()
    {
        resumeButtonImage.color = Color.white;
        resumeButtonImage.GetComponent<UISoundPlayer>().enabled = true;
        resumeButtonImage.GetComponent<Button>().enabled = true;
    }
}
