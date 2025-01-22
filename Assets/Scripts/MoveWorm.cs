using UnityEngine;

public class MoveWorm : MonoBehaviour
{
    [SerializeField]
    private float maxForwardSpeed;
    private float currentForwardSpeed = 0;
    private bool accelerate = true;

    // laneIndex : index des voies : -1=gauche, 0=milieu, 1=droite
    private int laneIndex = 0;

    private Transform wormContainerTransform;

    public bool isLeftSideObstructed = false;
    public bool isRightSideObstructed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wormContainerTransform = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        // Gestion du d�placement en avant
        if (accelerate)
        {
            // Augmentation graduelle de la vitesse de d�placement jusqu'au max
            currentForwardSpeed = Mathf.Clamp(currentForwardSpeed + .1f, 0, maxForwardSpeed);
            // D�placement Z du parent direct, contenant aussi le spot et la cam�ra
            wormContainerTransform.Translate(currentForwardSpeed * Time.deltaTime * Vector3.forward);
        }

        // Gestion des inputs pour changement de voie
        if (Input.GetKeyDown(KeyCode.LeftArrow) && laneIndex > -1 && !isLeftSideObstructed)
        {
            StartCoroutine(LaneUtils.SwitchLane(transform, --laneIndex));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && laneIndex < 1 && !isRightSideObstructed)
        {
            StartCoroutine(LaneUtils.SwitchLane(transform, ++laneIndex));
        }
    }

    #region Gestion des collisions [
    //-------------------------------
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Worm collision with : " + other.gameObject.name + " - (" + other.tag+")");
        switch(other.tag)
        {
            case "Obstacles/Rock" :
                accelerate = false;
                currentForwardSpeed = 0;
                break;
            
            case "Obstacles/Slug Trail" :
                accelerate = false;
                currentForwardSpeed = 0;
                break;

            case "Chaser" :
                Debug.Log("GAME OVER");
                //TODO : GAME OVER
                // R�duit la vitesse du poursuivant � 0 et l'arr�te
                MoveChaser mc = other.transform.parent.GetComponent<MoveChaser>();
                mc.accelerate = false;
                mc.currentForwardSpeed = 0;

                // R�duit la vitesse du ver � 0 et l'arr�te
                accelerate = false;
                currentForwardSpeed = 0;
                break;

            case "Collectables/Droplet" :
                //TODO : augmenter humidit� etc.
                other.GetComponent<Droplet>().Collect();
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacles/Rock"))
        {
            accelerate = true;
        }
    }
    //-------------------------------
    #endregion ] Gestion des collisions
}
