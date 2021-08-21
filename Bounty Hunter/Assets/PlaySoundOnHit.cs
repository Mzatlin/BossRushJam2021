using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnHit : MonoBehaviour
{
    AudioManager hitAudio;
    IHittablle hit;
    // Start is called before the first frame update
    void Start()
    {
        hitAudio = FindObjectOfType<AudioManager>();
        hit = GetComponent<IHittablle>();
        if (hit != null)
        {
            hit.OnHit += HandleHit;
        }
    }

    void OnDestroy()
    {
        hit.OnHit -= HandleHit;
    }

    private void HandleHit()
    {
        if (hitAudio != null)
        {
            hitAudio.PlayAudioByString("Play_Player_Damaged", gameObject);
        }
    }

}
