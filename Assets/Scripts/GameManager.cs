using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGamePaused { get; private set; }
    public bool IsGameOver { get; private set; }

    private float _Score;
    public float Score => Mathf.Floor(_Score); // readonly

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        IsGameOver = false;
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
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

    public void AddToScore(float scoreToAdd)
    {
        _Score += scoreToAdd;
    }

    //public void ResetScore()
    //{
    //    _Score = 0;
    //}
}
