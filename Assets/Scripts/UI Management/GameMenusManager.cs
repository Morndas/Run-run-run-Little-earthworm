using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenusManager : MonoBehaviour
{
    [SerializeField] private PersistentDataManager persistentDataManager;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject gameOverMenuUI;

    [SerializeField] private TextMeshProUGUI[] currentScoreUIElements;
    [SerializeField] private TextMeshProUGUI topScoreUIElement;

    private bool isGameStopped = false;

    private void Start()
    {
        // Chargement du top score enregistré dans l'élément d'UI du menu de Game Over
        SetTopScoreUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentScoreUI();

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

    #region MAJ des indicateurs de score (actuel et top score) [
    public void UpdateCurrentScoreUI()
    {
        foreach (TextMeshProUGUI currentScoreEl in currentScoreUIElements)
        {
            UIUtils.ChangeScoreUI(currentScoreEl, GameManager.Instance.Score);
        }
    }

    public void SetTopScoreUI()
    {
        UIUtils.ChangeScoreUI(topScoreUIElement, persistentDataManager.LoadTopScore());
    }
    #endregion ] MAJ des indicateurs de score (actuel et top score)
}

