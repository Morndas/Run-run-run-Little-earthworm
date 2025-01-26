using UnityEngine;
using UnityEngine.SceneManagement;

// TODO : refactor tous les scripts de menu en Game Manager
public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject GameOverMenuUI;

    public static bool IsGameOver { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsGameOver = false;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver)
        {
            StopGame();
        }
    }

    public void StopGame()
    {
        Time.timeScale = 0f;
        GameOverMenuUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Debug.Log("Quit application"); // debug
        Application.Quit();
    }
}
