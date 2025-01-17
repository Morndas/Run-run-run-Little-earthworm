using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorm : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject worm;

    private wormPosition = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MoveForward());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            worm.transform.localPosition += new Vector3(4, 0, 0);
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            worm.transform.localPosition -= new Vector3(4, 0, 0);
        }

    }

    IEnumerator MoveForward()
    {
        while (true)
        {
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
