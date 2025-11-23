using System;
using UnityEngine;

public class StartingScene : MonoBehaviour
{
    [SerializeField] public static Action startingSceneFinished;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnVideoFinished()
    {
        // start special crystal collect animation
        // when animation is finsihed, invoke the following action:

        // trigger scene transition
        startingSceneFinished?.Invoke();
    }
}
