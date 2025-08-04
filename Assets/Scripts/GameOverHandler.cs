using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        if (health != null)
        {
            health.onDeath.AddListener(OnPlayerDeath);
        }
    }

    void OnPlayerDeath()
    {
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
    }
}