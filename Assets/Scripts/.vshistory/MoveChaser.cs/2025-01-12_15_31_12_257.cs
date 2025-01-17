using System.Collections;
using UnityEngine;

public class MoveChaser : MonoBehaviour
{
    [SerializeField]
    private Transform wormTransform;

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
            // Augmentation graduelle de la vitesse de d�placement jusqu'au max
            currentForwardSpeed = Mathf.Clamp(currentForwardSpeed + 0.1f, 0, maxForwardSpeed);
            // D�placement Z du parent direct, contenant aussi le spot et la cam�ra
            transform.Translate(wormTransform.position.x, 0, currentForwardSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
