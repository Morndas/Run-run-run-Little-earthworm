using System.Collections;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private int laneIndex = 0;

    // LANE_SIZE : taille X d'un segment 
    private const int LANE_SIZE = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MoveForward());
    }

    // Update is called once per frame
    void Update()
    {
        bool changeLane = false;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && laneIndex > -1)
        {
            laneIndex--;
            changeLane = true;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && laneIndex < 1) {
            laneIndex++;
            changeLane = true;
        }

        if (changeLane)
        {
            transform.localPosition = new Vector3(laneIndex * LANE_SIZE, transform.localPosition.y, transform.localPosition.z);
        }
    }

    IEnumerator MoveForward()
    {
        while (true)
        {
            // Déplacement du parent direct "Worm Container", contenant aussi le spot et la caméra
            transform.parent.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
