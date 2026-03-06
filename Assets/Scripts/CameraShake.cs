using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    Vector3 originalPos;

    public void Shake(float duration, float magnitude, float decreaseFactor)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude, decreaseFactor));
    }

    IEnumerator ShakeCoroutine(float duration, float magnitude, float decreaseFactor)
    {
        originalPos = new Vector3(0, 0, -10);

        float elapsed = 0f;
        float currentMagnitude = magnitude;

        while (elapsed < duration && currentMagnitude > 0f)
        {
            float x = Random.Range(-1f, 1f) * currentMagnitude;
            float y = Random.Range(-1f, 1f) * currentMagnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;

            currentMagnitude -= decreaseFactor * Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
