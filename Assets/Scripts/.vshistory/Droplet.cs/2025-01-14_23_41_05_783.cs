using System.Collections;
using UnityEngine;

public class CollectDroplet : MonoBehaviour
{
    private float spinSpeed = 360f;
    private float fadeDuration = 1f;

    private Material material;
    private bool isCollected = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        material = GetComponent<Renderer>().material; // Assumes the droplet has a Renderer
    }

    public void Collect()
    {
        if (!isCollected)
        {
            isCollected = true;
            StartCoroutine(FadeOutAndDestroy());
        }
    }

    private IEnumerator FadeOutAndDestroy()
    {
        float elapsedTime = 0f;
        Color ogColor = material.color;

        while (elapsedTime < fadeDuration)
        {
            // Spin the droplet
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);

            // Fade out
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            material.color = new Color(ogColor.r, ogColor.g, ogColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        material.color = new Color(ogColor.r, ogColor.g, ogColor.b, 0f);
        Destroy(gameObject);
    }
}
