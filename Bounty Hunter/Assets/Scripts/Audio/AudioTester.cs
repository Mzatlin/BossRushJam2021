using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
    AudioManager audioManager;
    bool isPlaying;
    [SerializeField] AK.Wwise.Event[] audioEvents;
    
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayAudioWithCallback(audioEvents[1], null);
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
                    audioManager.PlayAudioByEvent(audioEvents[0], null);
                    isPlaying = false;
                }
                else
                {
                    audioManager.PlayAudioWithCallback(audioEvents[1], null);
                    isPlaying = true;
                }
            }
        }
    }
}
