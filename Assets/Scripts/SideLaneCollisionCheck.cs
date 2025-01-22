using UnityEngine;

public class SideLaneCollisionCheck : MonoBehaviour
{
    [SerializeField]
    private MoveWorm moveWorm;
    private string colliderSide; // "L" (gauche) ou "R" (droite)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Détermine s'il s'agit du collider gauche ou droit
        if (transform.localPosition.x < 0)
        {
            colliderSide = "L";
        } else
        {
            colliderSide = "R";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Path Blocking Obstacles"))
        {
            if (colliderSide == "L")
            {
                moveWorm.isLeftSideObstructed = true;
            } else
            {
                moveWorm.isRightSideObstructed = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Path Blocking Obstacles"))
        {
            if (colliderSide == "L")
            {
                moveWorm.isLeftSideObstructed = false;
            }
            else
            {
                moveWorm.isRightSideObstructed = false;
            }
        }
    }
}
