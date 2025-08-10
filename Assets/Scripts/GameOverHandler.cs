using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    
    void Start()
    {
            health.onDeath.AddListener(OnPlayerDeath);
    }

    void OnPlayerDeath()
    {
        Debug.Log("Player died");
        GameManager.Instance.GameOver();

    }
}