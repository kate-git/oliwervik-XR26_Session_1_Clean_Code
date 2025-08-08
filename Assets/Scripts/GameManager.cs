using DefaultNamespace;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameStateHandler 
{
    public static GameManager Instance { get; private set; }

    // Game state variables
    public bool gameOver; //public bool gameOver = false;
    public float gameTime; // public float gameTime = 0f;

    public UnityEvent onGameOver;
    public UnityEvent<int> onGameWin;
    
    private IScoreProvider scoreProvider;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        scoreProvider = FindObjectOfType<PlayerScore>();
        gameOver = false;
        gameTime = 0f;
    }
    
    
    void Update()
    {
        if (gameOver) return;

        gameTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
            RestartGame();

        if (scoreProvider != null && scoreProvider.GetScore() >= 30)
        {
            WinGame();
        }
    }

    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;
        onGameOver?.Invoke();
        Invoke(nameof(RestartGame), 2f);
    }

    private void WinGame()
    {
        if (gameOver) return;

        gameOver = true;
        onGameWin?.Invoke(scoreProvider.GetScore());
        Invoke(nameof(RestartGame), 2f);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}