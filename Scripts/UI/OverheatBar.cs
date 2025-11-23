using UnityEngine;
using UnityEngine.UI;

public class OverheatBar : MonoBehaviour
{
    [SerializeField] Image overheatBarImage;
    [SerializeField] Image overheatedImage;
    [SerializeField] Image BackroundImage;
    private void OnEnable()
    {
        PlayerOverheatSystem.onOverheatInfoChanged += UpdateOverheatBar;
    }

    private void OnDisable()
    {
        PlayerOverheatSystem.onOverheatInfoChanged -= UpdateOverheatBar;
    }
    
    private void UpdateOverheatBar(float currentAmount, float maxAmount, bool isOverheated)
    {
        overheatBarImage.fillAmount = Mathf.Clamp(currentAmount / maxAmount, 0f, 1f);
       
        if (isOverheated)
        {
            overheatedImage.fillAmount = Mathf.Clamp(currentAmount / maxAmount, 0f, 1f);
        }
        else
        {
            overheatedImage.fillAmount = 0;
        }
    }
    
}
