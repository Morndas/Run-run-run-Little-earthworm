using System.Collections;
using TMPro;
using UnityEngine;

public class SpiderTrap : MonoBehaviour
{
    private const float FALL_DOWN_SPEED = 25f;

    [SerializeField] private GameObject spider;
    [SerializeField] private Collider spiderCollider;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FallAndCatchPrey(other));
        }
    }

    private IEnumerator FallAndCatchPrey(Collider other)
    {
        // Position cible = position y du ver et x, z de l'araignée (bouge pas)
        //TODO : recalculer, position imprécise
        Vector3 targetpos = new Vector3(spider.transform.position.x, (other.transform.position.y), spider.transform.position.z);
        
        float fallDistance = spider.transform.position.y - targetpos.y;
        float fallDuration = fallDistance / FALL_DOWN_SPEED;

        Quaternion startRotation = spider.transform.rotation;
        //Quaternion targetRotation = Quaternion.Euler(0, startRotation.eulerAngles.y, startRotation.eulerAngles.z); // MARCHE PAS, MAUVAISE ROTATION
        Quaternion targetRotation = Quaternion.Euler(0, 90, 0);

        float timeElapsed = 0f;

        while (spider.transform.position.y > other.transform.position.y)
        {
            timeElapsed += Time.deltaTime;

            spider.transform.position += FALL_DOWN_SPEED * Time.deltaTime * Vector3.down;

            spider.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, Mathf.Clamp01(timeElapsed / fallDuration));

            yield return null;
        }

        spider.transform.SetPositionAndRotation(targetpos, targetRotation);
    }
}
