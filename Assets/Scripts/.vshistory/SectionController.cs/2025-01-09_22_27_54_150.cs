using UnityEngine;

public class SectionController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] SectionPrefabs;

    private GameObject[] SectionsInScene;
    private float groundSize;
    private int numberOfSections = 4;

    private GameObject worm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SectionsInScene = new GameObject[numberOfSections];

        // Instanciation des sections de départ
        for (int i = 0; i < numberOfSections; i++)
        {
            int randomSection = Random.Range(0, SectionPrefabs.Length);
            SectionsInScene[i] = Instantiate(SectionPrefabs[randomSection]);
        }


        #region Positionnement des sols
        // Récupération de la taille z des sols
        groundSize = SectionsInScene[0].GetComponentInChildren<Transform>().Find("Road").transform.localScale.z;
        // Récupèration du gameObject du vaisseau
        worm = GameObject.Find("Ship");

        // Calcul de la position z du prochain sol à placer
        // zPos initiale = pos vaisseau + moitié de la taille du sol - 1.5 de marge pour éviter les bords visibles
        float zPos = worm.transform.position.z + (groundSize / 2) - 1.5f;

        foreach (var ground in SectionsInScene)
        {
            ground.transform.position = new Vector3(0, 0.2f, zPos);
            zPos += groundSize;
        }
        #endregion Positionnement des sols
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
