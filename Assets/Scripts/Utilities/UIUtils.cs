using System.Collections;
using TMPro;
using UnityEngine;

/*
 * Classe utilitaire pour les changer le score dans un élément TextMeshPro
 */
public static class UIUtils
{
    public static void ChangeScoreUI(float newScore, TextMeshProUGUI UIElement)
    {
        // Rajoute le score au texte de UI
        // ("Current Score : XX", "Final Score : XX" etc.)
        int scoreTextSeparator = UIElement.text.IndexOf(" : ");
        UIElement.text = UIElement.text.Substring(0, scoreTextSeparator) + " : " + newScore;
    }
}
