using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private PersistentDataManager persistentDataManager;
    [SerializeField] private TextMeshProUGUI topScoreUIElement;

    private void Start()
    {
        SetTopScoreUI();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        Debug.Log("Quit application"); // debug
        Application.Quit();
    }
    public void SetTopScoreUI()
    {
        UIUtils.ChangeScoreUI(persistentDataManager.LoadTopScore(), topScoreUIElement);
    }
}
