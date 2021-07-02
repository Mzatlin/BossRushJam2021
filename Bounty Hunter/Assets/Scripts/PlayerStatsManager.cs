using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour, IPlayerStats
{
    [SerializeField] PlayerStatsSO stats;

    public bool GetPlayerReadiness()
    {
        return stats.isReady;
    }

    public void SetPlayerReadiness(bool isready)
    {
        stats.isReady = isready;
    }
}
