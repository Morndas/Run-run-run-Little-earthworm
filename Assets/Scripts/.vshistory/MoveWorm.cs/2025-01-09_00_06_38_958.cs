using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorm : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject worm;

    private int lanePosition = 0;

    private const int laneSize = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MoveForward());
    }

    // Update is called once per frame
    void Update()
    {
        bool changePosition = false;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && lanePosition > -1)
        {
            lanePosition--;
            changePosition = true;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && lanePosition < 1) {
            lanePosition++;
            changePosition = true;
        }

        if (changePosition)
        {
            //TODO : corriger orientation
            worm.transform.localPosition = new Vector3(-lanePosition * 5, worm.transform.localPosition.y, worm.transform.localPosition.z);
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
