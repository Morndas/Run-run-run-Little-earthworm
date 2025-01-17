using System.Collections;
using UnityEngine;

public class MoveChaser : MonoBehaviour
{
    [SerializeField]
    private float maxForwardSpeed;
    private float currentForwardSpeed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MoveForward());
    }
    
    IEnumerator MoveForward()
    {
        while (true)
        {
            // Augmentation graduelle de la vitesse de déplacement jusqu'au max
            currentForwardSpeed = Mathf.Clamp(currentForwardSpeed + 0.1f, 0, maxForwardSpeed);
            // Déplacement Z du parent direct, contenant aussi le spot et la caméra
            transform.Translate(currentForwardSpeed * Time.deltaTime * Vector3.forward);

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
            isPathObstructed = true;
            currentForwardSpeed = 0;
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
