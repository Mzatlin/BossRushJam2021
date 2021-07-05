using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupPlayerOnAwake : MonoBehaviour
{
    IPlayerStats stats;
    // Start is called before the first frame update
    void Awake()
    {
        stats = GetComponent<IPlayerStats>();
        if(stats != null)
        {
            stats.SetPlayerReadiness(true);
            stats.SetPlayerDeath(false);
        }
    }

}
