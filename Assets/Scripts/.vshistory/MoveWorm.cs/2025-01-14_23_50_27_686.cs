using System.Collections;
using System.Linq.Expressions;
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
    private const float LANE_SWITCH_DURATION = .1f;
    private float keyPressDelay = 0;

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
        Vector3 rayPosL = new Vector3((wormCollider.bounds.center.x - LANE_SIZE_X), wormCollider.bounds.center.y, (wormCollider.bounds.center.z - wormCollider.bounds.size.z / 2));
        Vector3 rayPosR = new Vector3((wormCollider.bounds.center.x + LANE_SIZE_X), wormCollider.bounds.center.y, (wormCollider.bounds.center.z - wormCollider.bounds.size.z / 2));

        //TODO : corrige le bug de raycast qui détecte pas les obstacles au spam de touche
        if (keyPressDelay > .3f)
        {
            keyPressDelay = 0;
        } else if (keyPressDelay != 0)
        {
            keyPressDelay += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && laneIndex > -1 && keyPressDelay == 0)
        {
            keyPressDelay = Time.deltaTime;

            if (!Physics.Raycast(rayPosL, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfoL, wormCollider.bounds.size.z))
            {
                Debug.DrawRay(rayPosL, (transform.TransformDirection(Vector3.forward) * wormCollider.bounds.size.z), Color.green);
                StartCoroutine(SwitchLane(--laneIndex));
            }
            else
            {
                Debug.DrawRay(rayPosL, (transform.TransformDirection(Vector3.forward) * hitInfoL.distance), Color.red);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && laneIndex < 1 && keyPressDelay == 0)
        {
            keyPressDelay = Time.deltaTime;
            if (!Physics.Raycast(rayPosR, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfoR, wormCollider.bounds.size.z))
            {
                Debug.DrawRay(rayPosR, (transform.TransformDirection(Vector3.forward) * wormCollider.bounds.size.z), Color.green);
                StartCoroutine(SwitchLane(++laneIndex));
            }
            else
            {
                Debug.DrawRay(rayPosR, (transform.TransformDirection(Vector3.forward) * hitInfoR.distance), Color.red);
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

    private IEnumerator SwitchLane(int targetLineIndex)
    {
        float timeElapsed = 0;
        float originX = transform.localPosition.x;
        float targetX = targetLineIndex * LANE_SIZE_X;

        while (timeElapsed < LANE_SWITCH_DURATION)
        {
            Vector3 newPos = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
            newPos.x = Mathf.Lerp(originX, targetX, timeElapsed / LANE_SWITCH_DURATION);

            transform.localPosition = newPos;
            
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = new Vector3(targetX, transform.localPosition.y, transform.localPosition.z);
    }

    #region Gestion des collisions [
    //-------------------------------
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Worm collision with : " + other.gameObject.name + " - (" + other.tag+")");
        switch(other.tag)
        {
            case "Rock" :
                isPathObstructed = true;
                currentForwardSpeed = 0;
                break;

            case "Chaser" :
                Debug.Log("GAME OVER");
                //isPathObstructed = true;
                //currentForwardSpeed = 0;
                break;
            case "Collectables/Droplet" :
                Debug.Log("AAAAAAAAAAAAAAAAAAAA");
                //TODO : augmenter humidité etc.
                other.GetComponent<Droplet>().Collect();
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
