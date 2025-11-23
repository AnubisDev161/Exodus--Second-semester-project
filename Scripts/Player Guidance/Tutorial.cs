using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    [SerializeField] public static Action tutorialFinished;
    [SerializeField] public static Action tutorialStarted;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
       
    }

    private void Start()
    {
        tutorialStarted?.Invoke();
    }

    public void OnVideoFinished()
    {
        // start special crystal collect animation
        // when animation is finsihed, invoke the following action:

        // trigger scene transition
        tutorialFinished?.Invoke();
    }
}
