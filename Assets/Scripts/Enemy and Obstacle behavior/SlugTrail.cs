using System.Collections;
using UnityEngine;

public class SlugTrail : MonoBehaviour
{
    private const float FADE_IN_DURATION = .4f;
    private const float FADE_OUT_DURATION = 2.5f;

    [SerializeField] Material material;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<Renderer>().material;
        StartCoroutine(FadeInAndOut());
    }
    
    private IEnumerator FadeInAndOut()
    {
        #region Fade in [
        float elapsedTime = 0f;
        while (elapsedTime < FADE_IN_DURATION)
        {
            // Fade in
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / FADE_OUT_DURATION);
            material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        material.color = new Color(material.color.r, material.color.g, material.color.b, 1f);
        #endregion ] Fade in

        #region Fade out and destroy [
        elapsedTime = 0f;
        while (elapsedTime < FADE_OUT_DURATION)
        {
            // Fade out
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / FADE_OUT_DURATION);
            material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);

            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
        material.color = new Color(material.color.r, material.color.g, material.color.b, 0f);

        Destroy(transform.parent.gameObject);
        #endregion ] Fade out and destroy
    }
}
