using UnityEngine;
using UnityEngine.Events;

public class PlayerScore : MonoBehaviour, IScoreProvider
{
    public UnityEvent<int> onScoreChanged= new UnityEvent<int>();

    private int score = 0;
    
    public void AddScore(int value)
    {
        score += value;
        onScoreChanged?.Invoke(score);
    }

    public int GetScore() => score;
}