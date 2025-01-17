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

        // Instanciation des sections de d�part
        for (int i = 0; i < numberOfSections; i++)
        {
            int randomSection = Random.Range(0, SectionPrefabs.Length);
            SectionsInScene[i] = Instantiate(SectionPrefabs[randomSection]);
        }


        #region Positionnement des sections
        //------------------------------------
        // R�cup�ration de la taille z des sections
        sectionSizeZ = SectionsInScene[0].transform.Find("Ground/Ground Plane").GetComponent<Renderer>().bounds.size.z;
        Debug.Log("sectionSizeZ : " + sectionSizeZ);
        // R�cup�ration du gameObject du ver
        worm = GameObject.Find("Worm");

        // Calcul de la position z de la prochaine section � placer
        // zPos initiale = pos ver + moiti� de la taille de la section - 2 de marge
        float zPos = worm.transform.position.z + (sectionSizeZ / 2) - 2;

        foreach (var section in SectionsInScene)
        {
            section.transform.position = new Vector3(0, 0, zPos);
            zPos += sectionSizeZ;
        }
        //------------------------------------
        #endregion Positionnement des sections
    }

    // Update is called once per frame
    void Update()
    {
        // On parcours la liste des sections � l'envers
        for (int i = SectionsInScene.Length - 1; i >= 0; i--)
        {
            GameObject section = SectionsInScene[i];

            // Si la section est derri�re le ver et 6 de marge
            if (section.transform.position.z + (sectionSizeZ / 2) < worm.transform.position.z - 6)
            {
                // D�truit le sol d�pass�
                float zPos = section.transform.position.z;
                Destroy(section);

                // Instancie un nouveau sol al�atoire
                int randomGround = Random.Range(0, SectionPrefabs.Length);
                GameObject newGround = Instantiate(SectionPrefabs[randomGround]);
                // Positionne le sol cr�� en t�te des autres
                newGround.transform.position = new Vector3(0, 0.2f, zPos + (sectionSizeZ * numberOfSections));
                SectionsInScene[i] = newGround;
            }
        }
    }
}
