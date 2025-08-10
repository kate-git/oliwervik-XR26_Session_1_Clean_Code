using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealthProvider
{
    public UnityEvent onDeath= new UnityEvent();
    public UnityEvent<float> onHealthChanged= new UnityEvent<float>();

    [SerializeField] private float maxHealth = 30f;
    public float currentHealth;

    
    private void Start()
    {
        currentHealth = maxHealth;
        onHealthChanged.Invoke(currentHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        onHealthChanged.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            onDeath.Invoke();
        }
    }

    public float GetHealth() => currentHealth;
}