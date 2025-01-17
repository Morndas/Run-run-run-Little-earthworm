using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorm : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject worm;

    private int wormPosition = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MoveForward());
    }

    // Update is called once per frame
    void Update()
    {
        bool changePosition = false;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && wormPosition > -1)
        {
            wormPosition--;
            changePosition = true;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && wormPosition < 1) {
            wormPosition++;
            changePosition = true;
        }

        if (changePosition)
        {
            //TODO : corriger orientation
            worm.transform.localPosition = new Vector3(-wormPosition * 4, worm.transform.localPosition.y, worm.transform.localPosition.z);
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
