using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenusManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject gameOverMenuUI;

    private bool isGameStopped = false;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameOver && !isGameStopped)
        {
            StopGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.IsGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    #region Actions Pause Menu [
    public void PauseGame()
    {
        GameManager.Instance.PauseGame();
        pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
        pauseMenuUI.SetActive(false);
    }
    #endregion ] Actions Pause Menu


    #region Actions Game Over Menu [
    public void StopGame()
    {
        isGameStopped = true;
        gameOverMenuUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
        // IsGameOver et timeScale restaurés au Start() du GameManager
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    #endregion ] Actions Game Over Menu


    public void Quit()
    {
        Debug.Log("Quit application"); // debug
        Application.Quit();
    }
}
