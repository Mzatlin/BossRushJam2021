using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour, IPlayerStats
{
    [SerializeField] PlayerStatsSO stats;

    public bool GetPlayerReadiness()
    {
        return stats.isReady && !stats.isDead && !stats.isInDialogue; 
    }

    public void SetPlayerReadiness(bool isready)
    {
        stats.isReady = isready;
    }
    public void SetPlayerDeath(bool isdead)
    {
        stats.isDead = isdead;
    }
    public void SetPlayerDialogue(bool isInDialogue)
    {
        stats.isInDialogue = isInDialogue;
    }
}
