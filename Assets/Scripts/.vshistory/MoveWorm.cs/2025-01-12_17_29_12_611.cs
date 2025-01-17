using System.Collections;
using UnityEngine;

public class MoveWorm : MonoBehaviour
{
    [SerializeField]
    private float maxForwardSpeed;
    private float currentForwardSpeed = 0;
    private bool isPathObstructed = false;

    // laneIndex : index des voies : -1=gauche, 0=milieu, 1=droite
    private int laneIndex = 0;
    private const int LANE_SIZE_X = 5;

    private Transform wormContainerTransform;
    private Collider wormCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wormContainerTransform = transform.parent;
        wormCollider = gameObject.GetComponent<Collider>();
        StartCoroutine(MoveForward());
    }

    // Update is called once per frame
    void Update()
    {
        bool changeLane = false;

        Vector3 rayposL = new Vector3((transform.position.x - LANE_SIZE_X), transform.position.y, (transform.position.z - wormCollider.bounds.size.z / 2));
        Vector3 rayposRD = new Vector3((transform.position.x + LANE_SIZE_X), transform.position.y, (transform.position.z - wormCollider.bounds.size.z / 2));
        #region Raycast debugging
        if (Physics.Raycast(rayposL, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfoLD, wormCollider.bounds.size.z))
        {
            Debug.DrawRay(rayposL, (transform.TransformDirection(Vector3.forward) * hitInfoLD.distance), Color.red);
        }
        else
        {
            Debug.DrawRay(rayposL, (transform.TransformDirection(Vector3.forward) * wormCollider.bounds.size.z), Color.green);
        }
        if (Physics.Raycast(rayposRD, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfoRD, 5))
        {
            Debug.DrawRay(rayposRD, (transform.TransformDirection(Vector3.forward) * hitInfoRD.distance), Color.red);
        }
        else
        {
            Debug.DrawRay(rayposRD, (transform.TransformDirection(Vector3.forward) * wormCollider.bounds.size.z), Color.green);
        }
        #endregion Raycast debugging

        if (Input.GetKeyDown(KeyCode.LeftArrow) && laneIndex > -1)
        {
            if(!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out RaycastHit hitInfoL, 5))
            {
                laneIndex--;
                changeLane = true;
            }
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && laneIndex < 1) {
            if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out RaycastHit hitInfoR, 5))
            {
                laneIndex++;
                changeLane = true;
            }
        }

        if (changeLane)
        {
            // Déplacement X du vers sur une autre voie
            // TODO : transition sur la durée
            transform.localPosition = new Vector3(laneIndex * LANE_SIZE_X, transform.localPosition.y, transform.localPosition.z);
        }
    }

    private IEnumerator MoveForward()
    {
        while (true)
        {
            if (!isPathObstructed)
            {
                // Augmentation graduelle de la vitesse de déplacement jusqu'au max
                currentForwardSpeed = Mathf.Clamp(currentForwardSpeed + .1f, 0, maxForwardSpeed);
                // Déplacement Z du parent direct, contenant aussi le spot et la caméra
                wormContainerTransform.Translate(currentForwardSpeed * Time.deltaTime * Vector3.forward);
            }

            yield return null;
        }
    }

    #region Gestion des collisions [
    //-------------------------------
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Worm collision with : " + other.gameObject.name);
        switch(other.tag)
        {
            case "Rock" :
                isPathObstructed = true;
                currentForwardSpeed = 0;
                break;

            case "Chaser":
                Debug.Log("GAME OVER");
                //isPathObstructed = true;
                //currentForwardSpeed = 0;
                break;
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
