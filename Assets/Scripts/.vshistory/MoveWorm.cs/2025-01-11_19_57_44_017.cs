using System.Collections;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] public float moveSpeed;

    // laneIndex : index des voies : -1=gauche, 0=milieu, 1=droite
    private int laneIndex = 0;

    // LANE_SIZE : taille X des voies
    private const int LANE_SIZE = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MoveForward());
    }

    // Update is called once per frame
    void Update()
    {
        bool changeLane = false;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && laneIndex > -1)
        {
            laneIndex--;
            changeLane = true;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && laneIndex < 1) {
            laneIndex++;
            changeLane = true;
        }

        if (changeLane)
        {
            // Déplacement X du vers sur une autre voie
            // TODO : transition sur la durée
            transform.localPosition = new Vector3(laneIndex * LANE_SIZE, transform.localPosition.y, transform.localPosition.z);
        }
    }

    IEnumerator MoveForward()
    {
        while (true)
        {
            // Déplacement Z du parent direct, contenant aussi le spot et la caméra
            transform.parent.transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);

            yield return null;
        }
    }
}
