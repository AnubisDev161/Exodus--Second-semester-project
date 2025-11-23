using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController sceneController;
    [SerializeField] private Animator transitionAnimator;

    private static bool functionCalled = false;
    private static int lastLevel = 0;
    private void Awake()
    {
        functionCalled = false;
        // checks if it has one copy of the GameObject
       
        if (sceneController == null)
        {
            sceneController = this;
        }
    }

    private void OnEnable()
    {
        LevelGoal.levelFinished += SwitchToNextLevel;
        Tutorial.tutorialFinished += SwitchToNextLevel;

        PlayerHealth.onPlayerDeath += OnPlayerDied;
        SaveSurvivorsMission.questFailed += OnPlayerDied;
        StartingScene.startingSceneFinished += OnStartingSceneFinished;
    }

    private void OnPlayerDied()
    {
        lastLevel = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadSceneWithTransition("Death_Screen"));
    }

    private void OnDisable()
    {
        LevelGoal.levelFinished -= SwitchToNextLevel;
        Tutorial.tutorialFinished -= SwitchToNextLevel;
        PlayerHealth.onPlayerDeath -= OnPlayerDied;
        SaveSurvivorsMission.questFailed -= OnPlayerDied;
        StartingScene.startingSceneFinished -= OnStartingSceneFinished;
    }

    public void SwitchSceneByIndex(int sceneIndex)
    {
        if (!functionCalled)
        {
            functionCalled = true;
            StartCoroutine(LoadSceneWithTransition(sceneIndex));
        }
    }

    public void SwitchToSceneInstantly(string sceneName)
    {
        DisableCanvasGroup();
        SceneManager.LoadScene(sceneName);
        EnableCanvasGroup();
    }

    public void SwitchSceneByName(string name)
    {
        if (!functionCalled)
        {
            functionCalled = true;
            StartCoroutine(LoadSceneWithTransition(name));
        }
    }

    public void SwitchToNextLevel()
    {
        if (!functionCalled)
        {
            functionCalled = true;
            //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

            lastLevel = SceneManager.GetActiveScene().buildIndex + 1;
            
            StartCoroutine(LoadSceneWithTransition(SceneManager.GetActiveScene().buildIndex + 1)); //+1
        }
    }

    private IEnumerator LoadSceneWithTransition(int sceneIndex)
    {
        DisableCanvasGroup();
        transitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneIndex);
        transitionAnimator.SetTrigger("FadeOut");
        EnableCanvasGroup();
    }
    private IEnumerator LoadSceneWithTransition(string sceneName)
    {
        DisableCanvasGroup();
        transitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName);
        transitionAnimator.SetTrigger("FadeOut");
        EnableCanvasGroup();
    }

    private void DisableCanvasGroup()
    {
        if (GetComponentInChildren<CanvasGroup>() == null) return;
        
        GetComponentInChildren<CanvasGroup>().enabled = false;
    }
    
    private void EnableCanvasGroup()
    {
        if (GetComponentInChildren<CanvasGroup>() == null) return;
        
        GetComponentInChildren<CanvasGroup>().enabled = true;
    }

    public void ReloadLastLevel()
    {
        StartCoroutine(LoadSceneWithTransition(lastLevel));
    }

    public void OnStartingSceneFinished()
    {
        lastLevel = 0;
        StartCoroutine(LoadSceneWithTransition("MainMenu"));
    }

    public bool HasGameAlreadyStarted()
    {
        if(lastLevel >= 2)
        {
            return true;
        }

        return false;
    }
}
