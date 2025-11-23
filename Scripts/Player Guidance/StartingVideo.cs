using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class StartingVideo : MonoBehaviour
{
    [SerializeField] private UnityEvent videoFinished;
    [SerializeField] private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnStartingVideoFinished;
    }

    public void PlayStartingVideo()
    {
        videoPlayer.Play();
    }

    public void OnStartingVideoFinished(VideoPlayer vp)
    {
        videoFinished?.Invoke();
    }

    public void OnDisableStartingVideo()
    {
        gameObject.SetActive(false);
    }
}