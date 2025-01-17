using UnityEngine;

public class CollisionsWorm : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock"))
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

}
