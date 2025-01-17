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

        Vector3 rayPosL = new Vector3((wormCollider.bounds.center.x - LANE_SIZE_X), wormCollider.bounds.center.y, (wormCollider.bounds.center.z - wormCollider.bounds.size.z / 2));
        Vector3 rayPosR = new Vector3((wormCollider.bounds.center.x + LANE_SIZE_X), wormCollider.bounds.center.y, (wormCollider.bounds.center.z - wormCollider.bounds.size.z / 2));
        #region Raycast debugging
        if (Physics.Raycast(rayPosL, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfoLD, wormCollider.bounds.size.z))
        {
        }
        else
        {
        }
        if (Physics.Raycast(rayPosR, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfoRD, wormCollider.bounds.size.z))
        {
        }
        else
        {
        }
        #endregion Raycast debugging

        if (Input.GetKeyDown(KeyCode.LeftArrow) && laneIndex > -1)
        {
            if (!Physics.Raycast(rayPosL, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfoL, wormCollider.bounds.size.z))
            {
                Debug.DrawRay(rayPosL, (transform.TransformDirection(Vector3.forward) * wormCollider.bounds.size.z), Color.green);
                laneIndex--;
                StartCoroutine(SwitchLane(transform.position, laneIndex, 5));
            }
            else
            {
                Debug.DrawRay(rayPosL, (transform.TransformDirection(Vector3.forward) * hitInfoLD.distance), Color.red);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && laneIndex < 1)
        {
            if (!Physics.Raycast(rayPosR, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfoR, wormCollider.bounds.size.z))
            {
                Debug.DrawRay(rayPosR, (transform.TransformDirection(Vector3.forward) * wormCollider.bounds.size.z), Color.green);
                laneIndex++;
                StartCoroutine(SwitchLane(transform.position, laneIndex, 5));
            }
            else
            {
                Debug.DrawRay(rayPosR, (transform.TransformDirection(Vector3.forward) * hitInfoRD.distance), Color.red);
            }
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
    private IEnumerator SwitchLane(Vector3 originPos, int targetLineIndex, float duration)
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            Vector3 targetPos = new Vector3(targetLineIndex * LANE_SIZE_X, transform.position.y, transform.position.z);
            transform.localPosition = Vector3.Lerp(originPos, targetPos, timeElapsed / duration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = new Vector3(targetLineIndex * LANE_SIZE_X, transform.position.y, transform.position.z);
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
