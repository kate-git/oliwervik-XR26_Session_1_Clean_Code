using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Game state variables
    public bool gameOver; //public bool gameOver = false;
    public float gameTime; // public float gameTime = 0f;

    // UI elements directly managed by GameManager (tight coupling)
    [SerializeField]
    private TextMeshProUGUI gameStatusText;
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private GameObject gameOverPanel; 

    [SerializeField] private MonoBehaviour scoreProviderRaw;
    private IScoreProvider scoreProvider;

    void Start()
    {
        scoreProvider = scoreProviderRaw as IScoreProvider;
        if (scoreProvider == null)
        {
            Debug.LogError("Assigned object does not implement IScoreProvider");
        }
    }
    void Start()
    {
        // Initialize UI
        if (gameStatusText != null)
        {
            gameStatusText.text = "Game Started!";
        }
        if (playerScore == null)
        {
            playerScore = FindFirstObjectByType<PlayerScore>();
            if (playerScore == null)
            {
                Debug.LogError("GameManager cannot find PlayerScore component!");
            }
        }
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        Debug.Log("GameManager initialized.");
    }

    void Update()
    {
        if (!gameOver)
        {
            gameTime += Time.deltaTime;
            UpdateTimerUI();

            // Handles input for restarting the game (monolithic and poor separation of concerns)
            if (Input.GetKeyDown(KeyCode.R)) // R to Restart
            {
                RestartGame();
            }
            // Win condition (tightly coupled)
            if (playerScore.GetScore() >= 30) // Direct access to player score
            {
                WinGame();
            }
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.FloorToInt(gameTime).ToString() + "s";
        }
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            Debug.Log("Game Over!");
            if (gameStatusText != null)
            {
                gameStatusText.text = "GAME OVER!";
            }
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }

            Invoke(nameof(RestartGame), 2f); // Restart after 2 seconds
        }
    }

    private void WinGame()
    {
        if (!gameOver) // Ensure win can only happen once
        {
            gameOver = true;
            Debug.Log("You Win! Score: " + playerScore.GetScore()); // Direct access to player score
            if (gameStatusText != null)
            {
                gameStatusText.text = "YOU WIN! Score: " + playerScore.GetScore();
            }

            Invoke(nameof(RestartGame), 2f); // Restart after 2 seconds
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarting Game...");
        Time.timeScale = 1f; // Resume game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }
}