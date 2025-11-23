using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animation))]
public class TutorialSlideShowPanel : MonoBehaviour
{
    [SerializeField] private Image[] slides;
    [SerializeField] private UnityEvent slideShowFinished;
    private int slideIndex;
    private bool isFinished;

    private void Awake()
    {
        slideIndex = 0;
        EnableDesiredSlide(slideIndex);
    }

    public void ShowNextSlide()
    {
        if (isFinished) return;

        slideIndex++;
        if (slideIndex <= slides.Length - 1)
        {
            EnableDesiredSlide(slideIndex);
        }
        else
        {
            slideShowFinished?.Invoke();
            isFinished = true;
        }
    }

    private void EnableDesiredSlide(int slideShowIndex)
    {
        foreach (var slide in slides)
        {
            slide.gameObject.SetActive(false);
        }

        slides[slideShowIndex].gameObject.SetActive(true);
    }

    public void OnDisableSlidePanel()
    {
        gameObject.SetActive(false);
    }
}
