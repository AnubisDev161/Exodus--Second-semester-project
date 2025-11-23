using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour
{

    [SerializeField] private int desiredNPCAmount;
    [SerializeField] public static Action levelFinished;
    [SerializeField] private Animation remainingAnimation;
    [SerializeField] private Animation thanksForPlayingAnimation;
    [SerializeField] private TextMeshProUGUI levelCompleted;
    [SerializeField] private TextMeshProUGUI remaining;
    [SerializeField] private UnityEvent endFightOver;
    [SerializeField] private UnityEvent showLevelFinishedPopup;
    [SerializeField] private AudioSource levelFinishedSound;

    [SerializeField] private float fadeToCreditsTime;
    private int NPCsEntered;
    private bool playerWon = false;
    private int currentNpcAlive;
    private int currentSceneIndex;
    private int currentGroupToSave;

    public void Initialize(int desiredNPCAmount)
    {
        this.desiredNPCAmount = desiredNPCAmount;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentGroupToSave = SceneManager.sceneCountInBuildSettings - 1 - currentSceneIndex;
        levelFinishedSound.volume = SoundManager.GetSound(SoundType.LEVEL_FINISHED).volume;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ConvoyNPC>(out ConvoyNPC convoyNPC))
        {
            currentNpcAlive = ConvoyAndEnemyNotifier.instance.GetListLength();
            NPCsEntered++;
            if (NPCsEntered >= desiredNPCAmount && NPCsEntered >= currentNpcAlive && !playerWon && currentSceneIndex != SceneManager.sceneCountInBuildSettings - 1)
            {
                showLevelFinishedPopup?.Invoke();

                
                levelFinishedSound.Play();
                playerWon = true;
                StartCoroutine(SwitchSceneLevel());
            }
            else if (NPCsEntered >= desiredNPCAmount && NPCsEntered >= currentNpcAlive && !playerWon && currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
            {


                StartCoroutine(SwitchToCredits());
                playerWon = true;
            }
        }
    }

    public void PlayThanksForPlaying()
    {
        //  thanksForPlayingAnimation.Play("ThanksForPlaying");
        StartCoroutine(SwitchToCredits());
        endFightOver?.Invoke();
        levelFinishedSound.Play();
        playerWon = true;
    }

    private void SetupText()
    {
        if(SceneManager.GetActiveScene().name == "Level_01_BO")
        {
            levelCompleted.text = $"Group {1} saved!";
            remaining.text = $"{2} remaining groups to save.";
        }
        else if (SceneManager.GetActiveScene().name == "Level Blueprint 3")
        {
            levelCompleted.text = $"Group {2} saved!";
            remaining.text = $"{1} remaining groups to save.";
        }
        else if (SceneManager.GetActiveScene().name == "Level Blueprint 2 v2.0")
        {
            levelCompleted.text = $"Group {3} saved!";
            remaining.text = $"{0} remaining groups to save.";
        }
      

    }

    private IEnumerator SwitchSceneLevel()
    {
        yield return new WaitForSeconds(4f);
        levelFinished?.Invoke();
    }

    private IEnumerator SwitchToCredits()
    {
        yield return new WaitForSeconds(fadeToCreditsTime);

        // replace this name with credits
        SceneController.sceneController.SwitchToSceneInstantly("Credits");
    }
}
