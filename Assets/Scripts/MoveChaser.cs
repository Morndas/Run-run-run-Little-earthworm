using System;
using UnityEngine;

public class MoveChaser : MonoBehaviour
{
    [SerializeField]
    private Transform wormTransform;

    [SerializeField]
    private float maxForwardSpeed;
    public float currentForwardSpeed = 0;
    public Boolean accelerate = true;

    private void Update()
    {
        if (accelerate)
        {
            // Augmentation graduelle de la vitesse de déplacement jusqu'au max
            currentForwardSpeed = Mathf.Clamp(currentForwardSpeed + 0.1f, 0, maxForwardSpeed);
        }
        // Déplacement vers l'avant en Z 
        transform.Translate(currentForwardSpeed * Time.deltaTime * Vector3.forward);
        // Aligne la position X avec celle du ver 
        transform.position = new Vector3(wormTransform.position.x, transform.position.y, transform.position.z);
    }
}
