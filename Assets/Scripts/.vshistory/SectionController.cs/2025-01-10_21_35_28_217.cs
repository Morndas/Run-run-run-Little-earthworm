using System.Collections.Generic;
using UnityEngine;

public class SectionController : MonoBehaviour
{
    [SerializeField]
    private Dictionary<GameObject, float> WeightedSectionPrefabsLvl1;

    private GameObject[] SectionsInScene;
    private float sectionSizeZ;

    [SerializeField]
    private int numberOfSections = 4;

    private GameObject worm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SectionsInScene = new GameObject[numberOfSections];

        // Instanciation des sections de départ
        for (int i = 0; i < numberOfSections; i++)
        {
            int randomSection = Random.Range(0, SectionPrefabsLvl1.Length);
            SectionsInScene[i] = Instantiate(SectionPrefabsLvl1[randomSection]);
        }


        #region Positionnement des sections
        //------------------------------------
        // Récupération de la taille z des sections
        sectionSizeZ = SectionsInScene[0].transform.Find("Ground/Ground Plane").GetComponent<Renderer>().bounds.size.z;
        Debug.Log("sectionSizeZ : " + sectionSizeZ);
        // Récupèration du gameObject du ver
        worm = GameObject.Find("Worm");

        // Calcul de la position z de la prochaine section à placer
        // zPos initiale = pos ver + moitié de la taille de la section - 2 de marge
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
        // On parcours la liste des sections à l'envers
        for (int i = SectionsInScene.Length - 1; i >= 0; i--)
        {
            GameObject section = SectionsInScene[i];

            // Si la section est derrière le ver et 6 de marge
            if (section.transform.position.z + (sectionSizeZ / 2) < worm.transform.position.z - 6)
            {
                // Détruit la section dépassée
                float zPos = section.transform.position.z;
                Destroy(section);

                // Instancie une nouvelle section aléatoire
                //TODO : difficulté dynamique
                int randomGround = Random.Range(0, SectionPrefabsLvl1.Length);
                GameObject newGround = Instantiate(SectionPrefabsLvl1[randomGround]);

                // Positionne la section créée en tête des autres
                newGround.transform.position = new Vector3(0, 0, zPos + (sectionSizeZ * numberOfSections));
                SectionsInScene[i] = newGround;
            }
        }
    }
}
