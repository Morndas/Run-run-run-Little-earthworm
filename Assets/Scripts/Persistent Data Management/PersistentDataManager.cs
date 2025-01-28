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
    // TODO : am�lioration possible : ajout date et Pseudo du tenant du score
    public void SaveNewTopScore(float newTopScore)
    {
        ScoreData data = new ScoreData(newTopScore);

        // Conversion des champs publics de l'objet ScoreData et leurs valeurs en donn�es JSON (S�rialisation)
        string json = JsonUtility.ToJson(data);
        // Ecriture des donn�es JSON dans un fichier au chemin indiqu�
        File.WriteAllText(filePath, json);
    }

    public float LoadTopScore()
    {
        float topScore = 0f;

        try
        {
            // Lecture des donn�es JSON au chemin indiqu�
            string json = File.ReadAllText(filePath);

            // Conversion des donn�es JSON en un objet ScoreData (D�s�rialisation)
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);

            if (data != null)
            {
                // Score arrondi � l'entier inf�rieur pour affichage
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
