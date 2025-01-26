using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsGamePaused { get; set; }
    public bool IsGameOver { get; set; }
    public int Score { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame();
    }

    public void PauseGame()
    {
        IsGamePaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        IsGamePaused = false;
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        IsGameOver = false;
        Time.timeScale = 1f;
    }

    public void AddScore(int scoreToAdd)
    {
        Score += scoreToAdd;
    }

    public void ResetScore()
    {
        Score = 0;
    }
}
