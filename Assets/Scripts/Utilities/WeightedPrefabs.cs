using UnityEngine;

/**
 * Permet d'associer un poids aux diff�rents prefabs, influant sur leurs chances d'�tre instanci� lors du traitement de g�n�ration proc�durale.
 */
[System.Serializable]
public class WeightedPrefabs
{
    public GameObject prefab;
    public float weight;

    public WeightedPrefabs(GameObject prefab, float weight)
    {
        this.prefab = prefab;
        this.weight = weight;
    }
}
