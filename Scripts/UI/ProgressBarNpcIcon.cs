using UnityEngine;

public class ProgressBarNpcIcon : MonoBehaviour
{
    public UnityEngine.UI.Image progressBar; // Reference to the progress bar image
    public RectTransform icon; // Reference to the icon's RectTransform
    private Vector2 iconOriginalPosition; // Store the icon's initial position

    private void Start()
    {
        iconOriginalPosition = icon.anchoredPosition;
    }
    public void OnUpdateIconPosition()
    {
        var fillAmount = progressBar.fillAmount;
        var newIconPos = new Vector2(iconOriginalPosition.x + (fillAmount * progressBar.rectTransform.rect.width), iconOriginalPosition.y);
        icon.anchoredPosition = newIconPos;
    }
}
