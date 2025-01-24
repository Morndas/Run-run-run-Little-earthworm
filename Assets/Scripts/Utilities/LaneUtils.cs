using System.Collections;
using UnityEngine;

/*
 * Classe utilitaire contenant les infos relatives aux voies et permettant de changer un transform de voie.
 */
public static class LaneUtils
{
    public const int LANE_SIZE_X = 5;
    private const float LANE_SWITCH_DURATION = .08f;

    public static IEnumerator SwitchLane(Transform transform, int targetLineIndex, float duration = LANE_SWITCH_DURATION)
    {
        float timeElapsed = 0;
        float originX = transform.localPosition.x;
        float targetX = targetLineIndex * LANE_SIZE_X;

        while (timeElapsed < duration)
        {
            Vector3 newPos = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
            newPos.x = Mathf.Lerp(originX, targetX, timeElapsed / duration);

            transform.localPosition = newPos;
            
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = new Vector3(targetX, transform.localPosition.y, transform.localPosition.z);
    }
}
