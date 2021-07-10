using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
    AudioManager audioManager;
    bool isPlaying;
    
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(audioManager)
            {
                audioManager.PlayAudioByString("Play_Gunshot", null);
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if(audioManager)
            {
                if (isPlaying)
                {
                    audioManager.PlayAudioByString("Stop_NightClubBoss", null);
                    isPlaying = false;
                }
                else
                {
                    audioManager.PlayAudioByString("Play_NightClubBoss", null);
                    isPlaying = true;
                }
            }
        }
    }
}
