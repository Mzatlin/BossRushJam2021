using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    Coroutine movement = null;
    Vector2 originalPosition;
    Vector2 endPos;
    Vector2 currentPos;
    float moveSpeed = 3f;
    Vector2 leftPosition;
    Vector2 rightPosition;
    bool isMoving;


    public void ResetMirror()
    {
        transform.position = originalPosition;
        SetupMirror();
    }

    public void SetMirrorStatus(bool setBool)
    {
        SetMovement(setBool);
        gameObject.SetActive(setBool);
    }

    public void SetMovement(bool setBool)
    {
        isMoving = setBool;
    }

    // Start is called before the first frame update
    void Awake()
    {
        isMoving = true;
        originalPosition = transform.position;
        SetupMirror();
    }

    void SetupMirror()
    {
        leftPosition = originalPosition + new Vector2(5, 0);
        rightPosition = originalPosition + new Vector2(-5, 0);
        currentPos = leftPosition;
    }

    private void Update()
    {

        float step = moveSpeed * Time.deltaTime; // calculate distance to move

        if (Vector2.Distance(transform.position, leftPosition) < 2f)
        {
            currentPos = rightPosition;
        }
        else if (Vector2.Distance(transform.position, rightPosition) < 2f)
        {
            currentPos = leftPosition;
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentPos, step);
        }
    }
}
