using System.Collections;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float maxForwardSpeed;
    public float currentForwardSpeed = 0;

    public bool isPathObstructed = false;

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
            if (!isPathObstructed)
            {
                // Déplacement Z du parent direct, contenant aussi le spot et la caméra
                transform.parent.transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);
            }

            yield return null;
        }
    }

    #region Gestion des collisions [
    //-------------------------------
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Worm collision with : " + other.gameObject.name);

        if (other.CompareTag("Rock"))
        {
            moveSpeed = 0;
            isPathObstructed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            isPathObstructed = false;
        }
    }
    //-------------------------------
    #endregion ] Gestion des collisions
}
