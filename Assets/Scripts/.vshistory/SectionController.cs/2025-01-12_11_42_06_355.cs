using UnityEngine;

public class SectionController : MonoBehaviour
{
    [SerializeField]
    // Prefabs de sections, avec des chances diff�rentes d'�tre choisies lors de la g�n�ration (ex : poids de 2 compte comme deux entr�es)
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

        // Instanciation des sections de d�part (premier de base puis al�atoire)
        SectionsInScene[0] = Instantiate(lvl1Sections[0].prefab);
        for (int i = 1; i < numberOfSections; i++)
        {
            SectionsInScene[i] = Instantiate(GetRandomSection());
        }


        #region Positionnement des sections [
        //------------------------------------
        // R�cup�ration de la taille z des sections
        sectionSizeZ = SectionsInScene[0].transform.Find("Ground/Ground Plane").GetComponent<Renderer>().bounds.size.z;
        // R�cup�ration du gameObject du ver
        worm = GameObject.Find("Worm");

        // Calcul de la position z de la prochaine section � placer
        // zPos initiale = pos ver + moiti� de la taille de la section - 2 de marge
        float zPos = worm.transform.position.z + (sectionSizeZ / 2) - 5;

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
                GameObject newGround = Instantiate(GetRandomSection());

                // Positionne la section cr��e en t�te des autres
                newGround.transform.position = new Vector3(0, 0, zPos + (sectionSizeZ * numberOfSections));
                SectionsInScene[i] = newGround;
            }
        }
    }

    private GameObject GetRandomSection()
    {
        GameObject randomSection = null;

        // Calcul du poids total pour toutes les sections
        float totalWeight = 0;
        foreach (var section in lvl1Sections)
        {
            totalWeight += section.weight;
        }

        // G�n�ration d'un nombre al�atoire d'apr�s le poids total
        float randomWeight = Random.Range(0, totalWeight);

        float cumulativeWeight = 0;
        // Le cumul des poids d�termine la section choisie avec le random
        foreach (var section in lvl1Sections)
        {
            cumulativeWeight += section.weight;
            if (randomWeight < cumulativeWeight)
            {
                randomSection = section.prefab;
                break;
            }
        }

        return randomSection;
    }
}
