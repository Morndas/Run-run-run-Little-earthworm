using UnityEngine;

/**
 * Permet d'associer un poids aux différents prefabs, influant sur leurs chances d'être instancié lors du traitement de génération procédurale.
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
