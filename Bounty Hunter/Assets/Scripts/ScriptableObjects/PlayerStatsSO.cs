using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    public bool isReady = true;
    public bool isDead = false;
    public bool isInDialogue = false;
}
