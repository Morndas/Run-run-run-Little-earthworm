using System.Collections;
using UnityEngine;

public class Droplet : MonoBehaviour
{
    private float spinSpeed = 5000;
    private float riseSpeed = 12f;
    private float fadeDuration = 0.2f;

    private Material[] materials;
    private Color[] ogColors;
    private bool isCollected = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        materials = new Material[renderers.Length];
        for (int i = 0 ; i < renderers.Length ; i++)
        {
            Debug.Log(renderers[i].name);
            materials[i] = renderers[i].material;
        }
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

        while (elapsedTime < fadeDuration)
        {
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            transform.Translate(0, (riseSpeed * Time.deltaTime), 0);

            // Fade out
            //float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            float t = elapsedTime / fadeDuration;
            float alpha = t * t;

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].color = new Color(materials[i].color.r, materials[i].color.g, materials[i].color.b, alpha);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = new Color(materials[i].color.r, materials[i].color.g, materials[i].color.b, 0f);
        }
        Destroy(gameObject);
    }
}
