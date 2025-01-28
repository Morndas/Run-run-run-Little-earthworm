using System.IO;
using UnityEngine;

public class PersistentDataManager : MonoBehaviour
{
    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "topScore.json");
    }

    #region Sauvegarde et chargement du Top Score [
    // TODO : amélioration possible : ajout date et Pseudo du tenant du score
    public void SaveNewTopScore(float newTopScore)
    {
        ScoreData data = new ScoreData(newTopScore);

        // Conversion des champs publics de l'objet ScoreData et leurs valeurs en données JSON (Sérialisation)
        string json = JsonUtility.ToJson(data);
        // Ecriture des données JSON dans un fichier au chemin indiqué
        File.WriteAllText(filePath, json);
    }

    public float LoadTopScore()
    {
        float topScore = 0f;

        try
        {
            // Lecture des données JSON au chemin indiqué
            string json = File.ReadAllText(filePath);

            // Conversion des données JSON en un objet ScoreData (Désérialisation)
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);

            if (data != null)
            {
                // Score arrondi à l'entier inférieur pour affichage
                topScore = Mathf.Floor(data.topScore);
            }
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("Fichier de sauvegarde inexistant : " + e.Message);
        }

        return topScore;
    }
    #endregion ] Sauvegarde et chargement du Top Score
}
