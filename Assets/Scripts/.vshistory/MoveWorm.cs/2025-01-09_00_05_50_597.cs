using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorm : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject worm;

    private int wormLane = 0;

    private const int laneSize

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MoveForward());
    }

    // Update is called once per frame
    void Update()
    {
        bool changePosition = false;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && wormLane > -1)
        {
            wormLane--;
            changePosition = true;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && wormLane < 1) {
            wormLane++;
            changePosition = true;
        }

        if (changePosition)
        {
            //TODO : corriger orientation
            worm.transform.localPosition = new Vector3(-wormLane * 5, worm.transform.localPosition.y, worm.transform.localPosition.z);
        }
    }

    IEnumerator MoveForward()
    {
        while (true)
        {
            //TODO : corriger orientation
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
