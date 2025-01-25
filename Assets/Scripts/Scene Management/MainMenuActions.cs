using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuActions : MonoBehaviour
{
    public void OnClickPlay()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickQuit()
    {
        Debug.Log("Quit application"); // debug
        Application.Quit();
    }
}
