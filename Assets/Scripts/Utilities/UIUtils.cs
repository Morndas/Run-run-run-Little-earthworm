using TMPro;

/*
 * Classe utilitaire pour les changer le score dans un élément TextMeshPro
 */
public static class UIUtils
{
    public static void ChangeScoreUI(TextMeshProUGUI UIElement, float newScore)
    {
        // Rajoute le score au texte de UI
        // ("Current Score : XX", "Final Score : XX" etc.)
        int scoreTextSeparator = UIElement.text.IndexOf(" : ");
        UIElement.text = UIElement.text.Substring(0, scoreTextSeparator) + " : " + newScore;
    }
}
