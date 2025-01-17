using UnityEngine;

public class SectionController : MonoBehaviour
{
    [SerializeField]
    private WeightedPrefabs[] lvl1Sections;

    private GameObject[] SectionsInScene;
    private float sectionSizeZ;

    [SerializeField]
    private int numberOfSections = 4;

    private GameObject worm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SectionsInScene = new GameObject[numberOfSections];

        // Instanciation des sections de d�part
        for (int i = 0; i < numberOfSections; i++)
        {
            int randomSection = Random.Range(0, lvl1Sections.Length);
            SectionsInScene[i] = Instantiate(lvl1Sections[randomSection].prefab);
        }


        #region Positionnement des sections [
        //------------------------------------
        // R�cup�ration de la taille z des sections
        sectionSizeZ = SectionsInScene[0].transform.Find("Ground/Ground Plane").GetComponent<Renderer>().bounds.size.z;
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
        #endregion ] Positionnement des sections
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
                // D�truit la section d�pass�e
                float zPos = section.transform.position.z;
                Destroy(section);

                // Instancie une nouvelle section al�atoire
                //TODO : difficult� dynamique
                int randomGround = Random.Range(0, lvl1Sections.Length);
                GameObject newGround = Instantiate(lvl1Sections[randomGround].prefab);

                // Positionne la section cr��e en t�te des autres
                newGround.transform.position = new Vector3(0, 0, zPos + (sectionSizeZ * numberOfSections));
                SectionsInScene[i] = newGround;
            }
        }
    }

    private GameObject GetRandomPrefab()
    {
        GameObject randomPrefab = null;

        float totalWeight = 0f;
        foreach (var section in lvl1Sections)
        {
            totalWeight += section.weight;
        }

        float randomWeight = Random.Range(0f, totalWeight);

        float cumulativeWeight = 0f;
        foreach (var entry in lvl1Sections)
        {
            cumulativeWeight += entry.weight;
            if (randomWeight < cumulativeWeight)
            {
                return entry.prefab;
            }
        }

        return null;
    }
}
}
