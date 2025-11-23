using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDetectable, IDamagable, IHitable
{
    // health Bar
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 10;

    public static Action onPlayerDeath;
    public static Action<float, float> updateHealth;
    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    
    [SerializeField] UnityEvent<Vector3> hitByBullet;
    
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void OnHit(int damage, Vector3 impactVector)
    {
        UpdateHealth(-damage);
        hitByBullet?.Invoke(impactVector);
    }

    public void Awake()
    {
        updateHealth?.Invoke(currentHealth, maxHealth);
    }

    public void Update()
    {
        if (gameObject.transform.position.y <= -20)
        {
            onPlayerDeath?.Invoke();
        }
    }

    public void UpdateHealth(int value)
    {
        currentHealth += value;
        updateHealth?.Invoke(currentHealth, maxHealth);
        SoundManager.PlaySound(SoundType.PLAYER_HURT);

        if (currentHealth <= 0)
        {
            onPlayerDeath?.Invoke();
        }
    }

    public bool isAlive()
    {
        return currentHealth > 0;
    }
}
