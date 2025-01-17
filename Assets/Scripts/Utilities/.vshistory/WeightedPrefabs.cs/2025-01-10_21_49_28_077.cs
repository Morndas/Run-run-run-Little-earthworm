using UnityEngine;

/**
 * Allows for procedural generation using prefabs with weighted chances to be instanciated.
 */
[System.Serializable]
public class WeightedPrefabs
{
    public GameObject prefab;
    public float weight;
}
