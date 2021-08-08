using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactiveAfterDuration : MonoBehaviour
{
    [SerializeField] GameObject inactive;
    [SerializeField] int timeDelay;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(timeDelay);
        if(inactive != null)
        {
            inactive.SetActive(false);
        }
    }
}
