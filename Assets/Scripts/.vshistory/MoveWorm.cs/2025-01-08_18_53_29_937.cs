using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorm : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MoveForward());
    }

    // Update is called once per frame
    void Update()
    {
        
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
