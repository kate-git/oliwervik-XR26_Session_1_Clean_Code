using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private PlayerScore score;
    [SerializeField] private PlayerHealth health;

    void Start()
    {
        healthBar.maxValue = 30f;
        health.onHealthChanged.AddListener(UpdateHealthBar);
        score.onScoreChanged.AddListener(UpdateScoreText);

        // Initial state
        UpdateScoreText(score.GetScore());
        UpdateHealthBar(health.GetHealth());
    }

    void UpdateScoreText(int newScore)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {newScore}";
    }

    void UpdateHealthBar(float newHealth)
    {
        if (healthBar != null)
            healthBar.value = newHealth;
    }
}