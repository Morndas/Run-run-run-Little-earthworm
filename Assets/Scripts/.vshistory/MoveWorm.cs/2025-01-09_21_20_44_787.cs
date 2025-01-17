using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorm : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject worm;

    private int laneIndex = 0;

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
            worm.transform.localPosition = new Vector3(laneIndex * LANE_SIZE, worm.transform.localPosition.y, worm.transform.localPosition.z);
        }
    }

    IEnumerator MoveForward()
    {
        while (true)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
