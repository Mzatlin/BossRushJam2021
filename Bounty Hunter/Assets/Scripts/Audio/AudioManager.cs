using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    public void PlayAudioByEvent(AK.Wwise.Event eventName, GameObject location)
    {
        if(location == null) { location = player; }
        if (eventName == null) { print("No audio event was selected for " + location); return; }

        eventName.Post(location);
    }

    public void PlayAudioByString(string eventName, GameObject location)
    {
        if (location == null) { location = player; }
        if (eventName == null) { print("No audio event was selected for " + location); return; }

        AkSoundEngine.PostEvent(eventName, location);
    }

}
