using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    Coroutine movement = null;
    [SerializeField] Vector2 originalPosition;
    [SerializeField] Vector2 endPos;
    Vector2 currentPos;
    float moveSpeed = 3f;
    Vector2 leftPosition;
    Vector2 rightPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        leftPosition = originalPosition + new Vector2(5, 0);
        rightPosition = originalPosition + new Vector2(-5, 0);
        currentPos = leftPosition;
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

    private void Update()
    {
       
        float step = moveSpeed * Time.deltaTime; // calculate distance to move

        if(Vector2.Distance(transform.position, leftPosition) < 2f)
        {
            currentPos = rightPosition;
        }
        else if (Vector2.Distance(transform.position, rightPosition) < 2f)
        {
            currentPos = leftPosition;
        }

        transform.position = Vector3.MoveTowards(transform.position, currentPos, step);

    }
}
