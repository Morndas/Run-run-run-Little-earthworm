using UnityEngine;

public class SectionController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] SectionPrefabs;

    private GameObject[] SectionsInScene;
    private float sectionSize;
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


        #region Positionnement des sections
        //------------------------------------
        // Récupération de la taille z des sections
        sectionSize = SectionsInScene[0].GetComponentInChildren<Transform>().Find("Ground").transform.localScale.z;
        // Récupèration du gameObject du vaisseau
        worm = GameObject.Find("Ship");

        // Calcul de la position z du prochain sol à placer
        // zPos initiale = pos vaisseau + moitié de la taille du sol - 1.5 de marge pour éviter les bords visibles
        float zPos = worm.transform.position.z + (sectionSize / 2) - 1.5f;

        foreach (var ground in SectionsInScene)
        {
            ground.transform.position = new Vector3(0, 0, zPos);
            zPos += sectionSize;
        }
        //------------------------------------
        #endregion Positionnement des sections
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
