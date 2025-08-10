using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameStateHandler 
{
    public static GameManager Instance { get; private set; }
    
    private IScoreProvider scoreProvider;
    public UnityEvent onDeath = new UnityEvent();
    public UnityEvent<int> onGameWin = new UnityEvent<int>();
    
    [SerializeField] private TextMeshProUGUI gameStatusText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private PlayerScore playerScore;
    [SerializeField] private PlayerHealth playerHealth;
    
    public float gameTime;
    public bool gameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        gameOver = false;
        gameTime = 0f;
        
        if (gameStatusText != null) gameStatusText.text = "Game Started!";
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        
        if (playerScore != null)
            playerScore.onScoreChanged.AddListener(OnScoreChanged);
    }
    
    
    void Update()
    {
        if (gameOver) return;

        gameTime += Time.deltaTime;
        if (timerText != null)
            timerText.text = "Time: " + Mathf.FloorToInt(gameTime) + "s";
        
        if (Input.GetKeyDown(KeyCode.R))
            RestartGame();
    }

    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;
        if (gameStatusText != null) gameStatusText.text = "GAME OVER!";
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        onDeath?.Invoke();
        Invoke(nameof(RestartGame), 2f);
    }

    public void WinGame()
    {
        if (gameOver) return;

        gameOver = true;
        if (gameStatusText != null) gameStatusText.text = "YOU WIN!";
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        
        onGameWin.Invoke(playerScore != null ? playerScore.GetScore() : 0);
        Invoke(nameof(RestartGame), 2f);
    }

    public void OnCollisionEnter()
    {
        if (gameOver) return;

        if (playerHealth != null && playerHealth.currentHealth <= 0)
            GameOver();
    }
    
    private void OnScoreChanged(int score)
    {
        if (score >= 30)
            WinGame();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);    }
}