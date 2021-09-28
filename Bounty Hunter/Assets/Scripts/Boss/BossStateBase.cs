using UnityEngine;
using System.Collections;
using System;

public abstract class BossStateBase : IState
{
    protected GameObject bossGameObject;

    public abstract Type Tick();
    public abstract void BeginState();
    public abstract void EndState();

    public BossStateBase(GameObject _gameObject)
    {
        bossGameObject = _gameObject;
    }

    protected virtual IEnumerator JumpTime(Vector2 endPos)
    {
        float lerpSpeed = 30f;
        Vector2 startPos = bossGameObject.transform.position;
        float totalDistance = Vector2.Distance(startPos, endPos);
        float fractionOfJourney = 0;
        float startTime = Time.time;

        if (Vector2.Distance(endPos, bossGameObject.transform.position) < 1)
        {
            yield return null;
        }
        else
        {
            while (fractionOfJourney < 1)
            {
                try
                {
                    fractionOfJourney = ((Time.time - startTime) * lerpSpeed) / totalDistance;
                    bossGameObject.transform.position = Vector2.Lerp(startPos, endPos, fractionOfJourney);
                }
                catch (Exception ex)
                {
                    Debug.Log("Unexpected jump occured during jump " + ex);
                    Debug.Log("Start pos " + startPos + " End pos " + endPos);
                }
                yield return null;
            }
        }
    }
}
