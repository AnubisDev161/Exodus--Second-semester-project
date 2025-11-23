using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class TutorialVideo: MonoBehaviour
{
    [SerializeField] private UnityEvent videoFinished;
    [SerializeField] private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnTutorialVideoFinished;
    }

    public void PlayTutorialVideo()
    {
        videoPlayer.Play();
    }

    public void OnTutorialVideoFinished(VideoPlayer vp)
    {
        videoFinished?.Invoke();
    }

    public void OnDisableTutorialVideo()
    {
        gameObject.SetActive(false);
    }
}
