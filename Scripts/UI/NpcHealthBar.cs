using UnityEngine;
using UnityEngine.UI;
public class NpcHealthBar : MonoBehaviour
{
    [SerializeField] private Image lowHealthImage;
    [SerializeField] private Image fullHealthImage;
    [SerializeField] private Canvas healthCanvas;
    public void OnUpdateNpcHealth(float health, float maxHealth)
    {
        if (health <= 0)
        {
            fullHealthImage.fillAmount = 0;
            lowHealthImage.fillAmount = 0;
        }
        else
        {
            fullHealthImage.fillAmount = (1 / maxHealth) * health;
            lowHealthImage.fillAmount = 1;
        }
    }

    public void OnEnableHealthBar(bool enable)
    {
        healthCanvas.enabled = enable;
    }
}

