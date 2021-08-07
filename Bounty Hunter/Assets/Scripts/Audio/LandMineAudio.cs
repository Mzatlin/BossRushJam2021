using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineAudio : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event audioEvent;
    AudioManager audioManager;
    
    public void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void PlayLandMineBeep()
    {
        if(audioManager != null)
        {
            audioManager.PlayAudioByEvent(audioEvent, gameObject);
        }      
    }
}
