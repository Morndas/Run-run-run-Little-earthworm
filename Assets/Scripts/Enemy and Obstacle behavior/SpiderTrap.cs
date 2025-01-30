using System.Collections;
using TMPro;
using UnityEngine;

public class SpiderTrap : MonoBehaviour
{
    [SerializeField] // debug
    private float FALL_DOWN_SPEED = 30f;

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
        //TODO : position imprécise
        Vector3 targetpos = new Vector3(spider.transform.position.x, (other.transform.position.y), spider.transform.position.z);
        Debug.Log("targetpos : " + targetpos);
        float fallDistance = spider.transform.position.y - targetpos.y;
        float fallDuration = fallDistance / FALL_DOWN_SPEED;

        Quaternion startRotation = spider.transform.rotation;
        //Quaternion targetRotation = Quaternion.Euler(0, startRotation.eulerAngles.y, startRotation.eulerAngles.z);
        Quaternion targetRotation = Quaternion.Euler(0, 90, 0);

        float timeElapsed = 0f;

        while (spider.transform.position.y > other.transform.position.y)
        {
            timeElapsed += Time.deltaTime;

            spider.transform.position += FALL_DOWN_SPEED * Time.deltaTime * Vector3.down;

            spider.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, Mathf.Clamp01(timeElapsed / fallDuration));

            yield return null;
        }

        spider.transform.position = targetpos;
        spider.transform.rotation = targetRotation;
    }
}
