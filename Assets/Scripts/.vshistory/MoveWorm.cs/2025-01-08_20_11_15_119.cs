using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorm : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] public GameObject worm;

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
            worm.transform.position -= new Vector3(2, worm.transform.position.y, worm.transform.position.z);
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {

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
