using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] gameScoreTextComponents;

    // Update is called once per frame
    void Update()
    {
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        foreach (TextMeshProUGUI csText in gameScoreTextComponents)
        {
            // Rajoute le score aux différents textes de UI
            // ("Current Score : XX", "Final Score : XX" etc.)
            int scoreTextSeparator = csText.text.IndexOf(" : ");
            csText.text = csText.text.Substring(0, scoreTextSeparator) + " : " + GameManager.Instance.Score;
        }
    }

}
