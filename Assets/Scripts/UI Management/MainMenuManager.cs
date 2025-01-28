using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private PersistentDataManager persistentDataManager;
    [SerializeField] private TextMeshProUGUI topScoreUIElement;

    private void Start()
    {
        // Chargement du top score enregistré dans l'élément d'UI
        UIUtils.ChangeScoreUI(topScoreUIElement, persistentDataManager.LoadTopScore());
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
}
