using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealthProvider
{
    [SerializeField] private float maxHealth = 30f;
    private float currentHealth;

    public UnityEvent onDeath;
    public UnityEvent<float> onHealthChanged;

    private void Start()
    {
        currentHealth = maxHealth;
        onHealthChanged?.Invoke(currentHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        onHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            onDeath?.Invoke();
        }
    }

    public float GetHealth() => currentHealth;
}