using DefaultNamespace;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    //[SerializeField] private GameManager gameManager;
    [SerializeField] private MonoBehaviour gameStateHandlerObject; // ссылка на GameManager
    private IGameStateHandler gameStateHandler;

    void Awake()
    {
        gameStateHandler = gameStateHandlerObject as IGameStateHandler;

        if (gameStateHandler == null)
            Debug.LogError("Assigned object does not implement IGameStateHandler");
    }
    
    void Start()
    {
        if (health != null)
        {
            health.onDeath.AddListener(OnPlayerDeath);
        }
    }

    void OnPlayerDeath()
    {
        gameStateHandler?.GameOver();

    }
}