using UnityEngine;

public class SectionController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] SectionPrefabs;

    private GameObject[] SectionsInScene;
    private float sectionSizeZ;
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
        //sectionSizeZ = SectionsInScene[0].GetComponentInChildren<Transform>().Find("Ground").transform.localScale.z;
        sectionSizeZ = SectionsInScene[0].transform.Find("Ground Plane").GetComponent<Collider>().bounds.size.z;
        Debug.Log("sectionSizeZ : " + sectionSizeZ);
        // Récupèration du gameObject du ver
        worm = GameObject.Find("Worm");

        // Calcul de la position z de la prochaine section à placer
        // zPos initiale = pos ver + moitié de la taille de la section - 2 de marge
        float zPos = worm.transform.position.z + (sectionSizeZ / 2) - 2;

        foreach (var ground in SectionsInScene)
        {
            ground.transform.position = new Vector3(0, 0, zPos);
            zPos += sectionSizeZ;
        }
        //------------------------------------
        #endregion Positionnement des sections
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
