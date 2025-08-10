using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gameStatusText;
    [SerializeField] private GameObject gameOverPanel;
    
    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (gameStatusText != null)
            gameStatusText.text = "Game Started!";
        
        GameManager.Instance.onGameOver.AddListener(ShowGameOver);
        GameManager.Instance.onGameWin.AddListener(ShowWin);
        
    }

    void Update()
    {
        if (!GameManager.Instance.gameOver)
            UpdateTimer(GameManager.Instance.gameTime);
    }

    private void UpdateTimer(float time)
    {
        if (timerText != null)
            timerText.text = $"Time: {Mathf.FloorToInt(time)}s";
    }

    private void ShowGameOver()
    {
        gameStatusText.text = "GAME OVER!";
        gameOverPanel.SetActive(true);
    }

    private void ShowWin(int score)
    {
        gameStatusText.text = $"YOU WIN! Score: {score}";
        gameOverPanel.SetActive(true);
    }
}