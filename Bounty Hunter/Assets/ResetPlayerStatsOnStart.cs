using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerStatsOnStart : MonoBehaviour
{
    [SerializeField] PlayerStatsSO stats;
    // Start is called before the first frame update
    void Start()
    {
        stats.ResetPlayerStats();
    }

}
