using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SectionController : MonoBehaviour
{
    // Nombre de sections � instancier
    private const int INSTANTIATED_SECTIONS_NB = 8;

    // Prefabs de sections, avec des chances diff�rentes d'�tre choisies lors de la g�n�ration (ex : poids de 2 compte comme deux entr�es)
    [SerializeField]
    private WeightedPrefabs[] lvl0Sections; // caillous
    [SerializeField]
    private WeightedPrefabs[] lvl1Sections; // limaces
    [SerializeField]
    private WeightedPrefabs[] lvl2Sections; // araign�es

    [SerializeField] // debug
    private int phaseNumber = 0;
    /* PHASE DE DIFFICULTE (augmente selon distance parcourue / score)
     * 
     * Phase 0 : caillous uniquement
     * Phase 1 : caillous + limaces (peu encombr�)
     * Phase 2 : caillous + limaces (tr�s encombr�)
     * Phase 3 : caillous + limaces + araign�es (peu encombr�)
     * Phase 4 : caillous + limaces + araign�es (tr�s encombr�)
     * etc.
     * 
     * Tous les no de phase imppairs : ajouter la prochaine liste de difficult� au total des obstacles
     * Tous les no de phase pairs : augmenter le poids de la derni�re liste d'obstacles (la plus difficile)
     */

    [SerializeField] // debug
    private WeightedPrefabs[] instantiableSections;
    [SerializeField] // debug
    private int nbOfSectionLevelsToUse = 1;

    private List<WeightedPrefabs[]> prefabsPerLvl; 

    private GameObject[] SectionsInScene;
    private float sectionSizeZ;

    private GameObject worm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        prefabsPerLvl = new List<WeightedPrefabs[]> { lvl0Sections, lvl1Sections, lvl2Sections };
        // On commence avec les caillous uniquement
        instantiableSections = prefabsPerLvl[0];

        SectionsInScene = new GameObject[INSTANTIATED_SECTIONS_NB];

        // Instanciation des sections de d�part (premier de base puis al�atoire)
        SectionsInScene[0] = Instantiate(instantiableSections[0].prefab);
        for (int i = 1; i < INSTANTIATED_SECTIONS_NB; i++)
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
        int newPhaseNumber = (int) (GameManager.Instance.Score / 300f); // On augmente de phase tous les 300 de score (=300m parcourus)

        // Si changement de phase : on �tablit quelles sections utiliser pour la g�n�ration
        if (newPhaseNumber > phaseNumber)
        {
            phaseNumber = newPhaseNumber;

            bool isPhaseNumberEven = (phaseNumber % 2 == 0);
            // Si num�ro de phase impair : une nouvelle difficult� sera ajout�e
            if (!isPhaseNumberEven && nbOfSectionLevelsToUse <= (prefabsPerLvl.Count - 1))
            {
                nbOfSectionLevelsToUse++;
            }

            instantiableSections = new WeightedPrefabs[0];
            // On reconstruit le set de sections instanciables.
            for (int i = 0; i < nbOfSectionLevelsToUse; i++)
            {
                //TODO : voir pour augmenter de + en + si diff max atteinte
                //WeightedPrefabs[] prefabsToAdd = (WeightedPrefabs[]) prefabsPerLvl[i].Clone();
                WeightedPrefabs[] prefabsToAdd = new WeightedPrefabs[prefabsPerLvl[i].Length];

                // Num�ro de phase pair : augmente pour dernier niveau ajout� le poid de chaque section
                if (i == (nbOfSectionLevelsToUse - 1) && isPhaseNumberEven)
                {
                    for (int j = 0; j < prefabsPerLvl[i].Length; j++)
                    {
                        // remplace la r�f�rence de l'object s�rialis� avec une copie
                        prefabsToAdd[j] = new WeightedPrefabs(prefabsPerLvl[i][j].prefab, prefabsPerLvl[i][j].weight);
                        prefabsToAdd[j].weight++;
                    }
                }
                else
                {
                    prefabsToAdd = prefabsPerLvl[i];
                }

                instantiableSections = instantiableSections.Concat(prefabsToAdd).ToArray();
            }
        }

        // Destruction des sections d�pass�es et instanciation de nouvelles
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
                newGround.transform.position = new Vector3(0, 0, zPos + (sectionSizeZ * INSTANTIATED_SECTIONS_NB));
                SectionsInScene[i] = newGround;
            }
        }
    }

    private GameObject GetRandomSection()
    {
        GameObject randomSection = null;

        // Calcul du poids total pour toutes les sections
        float totalWeight = 0;
        foreach (var section in instantiableSections)
        {
            totalWeight += section.weight;
        }

        // G�n�ration d'un nombre al�atoire d'apr�s le poids total
        float randomWeight = Random.Range(0, totalWeight);

        float cumulativeWeight = 0;
        // Le cumul des poids d�termine la section choisie avec le random
        foreach (var section in instantiableSections)
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
