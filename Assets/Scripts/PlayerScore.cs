using UnityEngine;
using UnityEngine.Events;

public class PlayerScore : MonoBehaviour, IScoreProvider
{
    private int score = 0;

    public UnityEvent<int> onScoreChanged;

    public void AddScore(int value)
    {
        score += value;
        onScoreChanged?.Invoke(score);
    }

    public int GetScore() => score;
}