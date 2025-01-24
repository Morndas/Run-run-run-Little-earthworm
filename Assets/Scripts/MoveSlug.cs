using System.Collections;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class MoveSlug : MonoBehaviour
{
    [SerializeField]
    private GameObject slugTrailPrefab;
    private GameObject worm;

    [SerializeField]
    private float distanceToStart = 110;

    private bool isCycling = false;
    private bool hasArrived = false;

    private string moveDirection; // "L" (va a gauche) ou "R" (va a droite)
    private int laneIndex;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        worm = GameObject.FindWithTag("Player");

        laneIndex = Mathf.RoundToInt(transform.localPosition.x / LaneUtils.LANE_SIZE_X);
        // Détermine si la limace doit aller a gauche ou a droite
        if (laneIndex < 0)
        {
            moveDirection = "R";
        }
        else
        {
            moveDirection = "L";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isCycling && Vector3.Distance(transform.position, worm.transform.position) <= distanceToStart)
        {
            isCycling = true;
            StartCoroutine(CycleLanes());
        }
    }

    private IEnumerator CycleLanes()
    {
        while (!hasArrived)
        {
            if (moveDirection == "R")
            {
                yield return StartCoroutine(LaneUtils.SwitchLane(transform, ++laneIndex, .2f));
                hasArrived |= (laneIndex == 2);
            } else
            {
                yield return StartCoroutine(LaneUtils.SwitchLane(transform, --laneIndex, .2f));
                hasArrived |= (laneIndex == -2);
            }


            Instantiate(slugTrailPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }
}
