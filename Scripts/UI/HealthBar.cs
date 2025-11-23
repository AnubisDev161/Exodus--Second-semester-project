using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image HealthBarImage;
    private float currentPlayerHealth;
    private float maxPlayerHealth;
    
    private void OnUpdateHealth(float currentHealth, float maxPlayerHealth)
    {
        currentPlayerHealth = currentHealth;
        this.maxPlayerHealth = maxPlayerHealth;
        HealthBarImage.fillAmount = Mathf.Clamp(currentPlayerHealth / maxPlayerHealth, 0, 1); 
    }

    private void OnEnable()
    {
        PlayerHealth.updateHealth += OnUpdateHealth;
    }

    private void OnDisable()
    {
        PlayerHealth.updateHealth -= OnUpdateHealth;
    }
}
