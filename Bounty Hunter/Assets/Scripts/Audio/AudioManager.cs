using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Image background;

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

    public void PlayAudioWithCallback(AK.Wwise.Event eventName, GameObject location)
    {
        if (location == null) { location = player; }
        if (eventName == null) { print("No audio event was selected for " + location); return; }

        eventName.Post(location, (uint)AkCallbackType.AK_MusicSyncBeat, BackgroundColorChanger);
    }

    private void BackgroundColorChanger(object in_cookie, AkCallbackType in_type, object in_info)
    {
        background.color = Random.ColorHSV();
    }
}
