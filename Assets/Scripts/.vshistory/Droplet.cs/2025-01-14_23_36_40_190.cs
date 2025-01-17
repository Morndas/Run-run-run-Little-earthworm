using UnityEngine;

public class CollectDroplet : MonoBehaviour
{
    private float spinSpeed = 360f; // Degrees per second
    private float fadeDuration = 1f;

    private Renderer renderer; // Droplet material for fading
    private bool isCollected = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        renderer = GetComponent<Renderer>().material; // Assumes the droplet has a Renderer
    }

    public void Collect()
    {
        if (isCollected) return; // Avoid multiple calls
        isCollected = true;

        // Start fading and spinning
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        float elapsedTime = 0f;
        Color originalColor = material.color;

        while (elapsedTime < fadeDuration)
        {
            // Spin the droplet
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);

            // Fade out
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure fully transparent and destroy the droplet
        material.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        Destroy(gameObject);
    }
}
