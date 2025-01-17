using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class DrawGroundLines : MonoBehaviour
{
    [SerializeField] public float posX;
    [SerializeField] private GameObject ground;

    private LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Collider groundCollider = ground.GetComponent<Collider>();
        float startZ = ground.transform.position.z - (groundCollider.bounds.size.z / 2);
        float endZ = ground.transform.position.z + (groundCollider.bounds.size.z / 2);

        Vector3 startPoint = new Vector3(posX, .1f, startZ);
        Vector3 endPoint = new Vector3(posX, .1f, endZ);

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
