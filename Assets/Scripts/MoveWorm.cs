using System.Collections;
using UnityEngine;

public class MoveWorm : MonoBehaviour
{
    public bool isLeftSideObstructed = false;
    public bool isRightSideObstructed = false;

    [SerializeField]
    private float maxForwardSpeed;
    private float currentForwardSpeed = 0;
    private bool accelerate = true;

    // laneIndex : index des voies : -1=gauche, 0=milieu, 1=droite
    private int laneIndex = 0;

    private Transform wormContainerTransform;

    private Coroutine accelCoroutine = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wormContainerTransform = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        // Gestion du déplacement en avant
        if (accelerate)
        {
            // Augmentation graduelle de la vitesse de déplacement jusqu'au max
            currentForwardSpeed = Mathf.Clamp(currentForwardSpeed + .1f, 0f, maxForwardSpeed);
        }

        // Déplacement Z du parent direct, contenant aussi le spot et la caméra
        float forwardMoveDistance = currentForwardSpeed * Time.deltaTime;
        wormContainerTransform.Translate(forwardMoveDistance * Vector3.forward);

        // Ajout au score de la distance du déplacement
        GameManager.Instance.AddToScore(forwardMoveDistance);
        // Gestion des inputs pour changement de voie
        if (!GameManager.Instance.IsGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && laneIndex > -1 && !isLeftSideObstructed)
            {
                StartCoroutine(LaneUtils.SwitchLane(transform, --laneIndex));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && laneIndex < 1 && !isRightSideObstructed)
            {
                StartCoroutine(LaneUtils.SwitchLane(transform, ++laneIndex));
            }
            //DEBUG
            else if (Input.GetKeyDown(KeyCode.Space) && currentForwardSpeed == 0)
            {
                accelerate = true;
                currentForwardSpeed = 30f;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                accelerate = false;
                currentForwardSpeed = 0;
            }
        }

    }

    #region Gestion des collisions [
    //-------------------------------
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Worm collision with : " + other.gameObject.name + " - (" + other.tag+")");
        switch(other.tag)
        {
            case "Obstacles/Rock" :
                DecreaseWormSpeed(0, false);
                break;
            
            case "Obstacles/Slug Trail" :
                DecreaseWormSpeed((maxForwardSpeed / 2), false);
                accelCoroutine = StartCoroutine(AccelerateAfterSeconds(1.5f));
                break;

            case "Chaser" :
                // GameOver : set le timeScale à 0 et menu de GO apparait (cf. GameMenusManager)
                GameManager.Instance.GameOver();
                break;

            case "Foe/Spider" :
                DecreaseWormSpeed(0, false);
                if (true) // test entortillage
                {
                    // GameOver : set le timeScale à 0 et menu de GO apparait (cf. GameMenusManager)
                    GameManager.Instance.GameOver();
                }
                break;

            case "Collectables/Droplet" :
                //TODO : augmenter humidité etc.
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

    private IEnumerator AccelerateAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        accelerate = true;
        accelCoroutine = null;
    }

    private void DecreaseWormSpeed(float newSpeed, bool newAccelerate)
    {
        currentForwardSpeed = Mathf.Min(currentForwardSpeed, newSpeed);

        accelerate = newAccelerate;

        // On arrête les coroutines en cours qui font accélerer le ver après X secondes
        if (!accelerate && accelCoroutine != null)
        {
            StopCoroutine(accelCoroutine);
            accelCoroutine = null;
        }
    }
}
