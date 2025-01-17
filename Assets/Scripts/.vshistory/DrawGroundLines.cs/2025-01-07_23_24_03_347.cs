using System.Net;
using UnityEngine;

public class DrawGroundLines : MonoBehaviour
{
    [SerializeField]
    public Vector3 startPoint = new Vector3(0, 0, 0);
    [SerializeField]
    public Vector3 endPoint = new Vector3(1, 0, 0);

    private LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
