using UnityEngine;

// TODO : Combiner a MoveWorm.cs ??
public class CollisionTest : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Worm collision with : " + other.gameObject.name);

        if (other.CompareTag("Rock"))
        {
            Move moveWorm = gameObject.GetComponent<Move>();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

}
