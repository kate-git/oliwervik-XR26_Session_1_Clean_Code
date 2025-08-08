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

        //subscription on event from GameManager
        GameManager.Instance.onGameOver.AddListener(ShowGameOver);
        GameManager.Instance.onGameWin.AddListener(ShowWin);
    }

    void Update()
    {
        if (!GameManager.Instance.gameOver)
            UpdateTimer(GameManager.Instance.gameTime);
    }

    public void UpdateTimer(float time)
    {
        if (timerText != null)
            timerText.text = $"Time: {Mathf.FloorToInt(time)}s";
    }

    public void ShowGameOver()
    {
        if (gameStatusText != null)
            gameStatusText.text = "GAME OVER!";
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void ShowWin(int score)
    {
        if (gameStatusText != null)
            gameStatusText.text = $"YOU WIN! Score: {score}";
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }
}