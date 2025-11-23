using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConvoyProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image progressBarImage;
    [SerializeField] private UnityEvent updateConvoyIconPosition;
    [SerializeField] private Animation progressBarAnimation;
    [SerializeField] private Animation progressBarIconAnimation;
    private void OnUpdateProgress(float currentProgress, float totalDistance)
    {
        progressBarImage.fillAmount = Mathf.Clamp(currentProgress / totalDistance, 0, 1);
        updateConvoyIconPosition?.Invoke();
    }

    private void OnEnable()
    {
        ConvoyProgressBar.updateProgress += OnUpdateProgress;
        ConvoyNPC.hit += OnNpcHit;
    }

    private void OnDisable()
    {
        ConvoyProgressBar.updateProgress -= OnUpdateProgress;
        ConvoyNPC.hit -= OnNpcHit; 
    }

    private void OnNpcHit()
    {
        progressBarAnimation.Play();
        progressBarIconAnimation.Play();
    }

    public void OnDisableHUDAndHeatBar()
    {
        gameObject.SetActive(false);
    }

}
