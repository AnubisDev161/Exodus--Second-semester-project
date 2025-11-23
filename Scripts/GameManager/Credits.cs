using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class Credits : MonoBehaviour
{
    [SerializeField] private PlayableDirector trackingShot;

    private void Awake()
    {
        trackingShot.stopped += OnTrackingShotFinished;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnCreditsFinished()
    {
        SceneController.sceneController.SwitchSceneByName("MainMenu");
    }

    private void OnTrackingShotFinished(PlayableDirector trackingShot)
    {
        OnCreditsFinished();
    }
}
