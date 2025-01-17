using UnityEngine;

public class CollisionTest : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Worm collision with : " + other.gameObject.name);

        if (other.CompareTag("Rock"))
        {
            MoveWorm m = gameObject.GetComponent<MoveWorm>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

}
