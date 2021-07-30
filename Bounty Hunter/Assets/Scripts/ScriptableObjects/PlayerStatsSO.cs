using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    public bool isReady = true;
    public bool isDead = false;
    public bool isInDialogue = false;
    public bool isPaused = false;

    public bool GetPlayerReadiness()
    {
        return isReady && !isDead && !isInDialogue && !isPaused;
    }

    public void ResetPlayerStats()
    {
        isReady = true;
        isDead = false;
        isInDialogue = false;
        isPaused = false;
    }
}
