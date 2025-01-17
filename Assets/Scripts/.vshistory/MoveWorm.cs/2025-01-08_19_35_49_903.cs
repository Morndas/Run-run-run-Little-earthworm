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
        if (Input.GetButton("Left"))
        {
            transform.Translate(Vector3.right + 4f);
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
