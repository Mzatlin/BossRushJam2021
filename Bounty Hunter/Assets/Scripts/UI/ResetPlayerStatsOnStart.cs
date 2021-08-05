using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerStatsOnStart : MonoBehaviour
{
    [SerializeField] PlayerStatsSO stats;
    [SerializeField] BossDialogueSO bossDialogue;
    // Start is called before the first frame update
    void Awake()
    {
        if(bossDialogue != null && !bossDialogue.isOpeningSet)
        {
            stats.ResetPlayerStats();
            stats.isInDialogue = true;
            stats.isReady = false;
        }
        else
        {
            stats.ResetPlayerStats();
        }

    }

}
