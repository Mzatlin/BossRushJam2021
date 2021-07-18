using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    Coroutine movement = null;
    [SerializeField] Vector2 originalPosition;
    [SerializeField] Vector2 endPos;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    public void MoveToPosition(Vector2 offset, float lerpSpeed)
    {
        if (movement != null)
        {
            StopCoroutine(movement);
        }
        movement = StartCoroutine(MoveTime(offset, lerpSpeed));
    }

    protected virtual IEnumerator MoveTime(Vector2 offset, float lerpSpeed)
    {
        Vector2 endPos = originalPosition;
        Vector2 startPos = transform.position;
        endPos = startPos + offset;
        Debug.Log(startPos + offset);

        float totalDistance = Vector2.Distance(startPos, endPos);
        float fractionOfJourney = 0;
        float startTime = Time.time;

        while (fractionOfJourney < 1)
        {
            fractionOfJourney = ((Time.time - startTime) * lerpSpeed) / totalDistance;
            transform.position = Vector2.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }
    }
}
