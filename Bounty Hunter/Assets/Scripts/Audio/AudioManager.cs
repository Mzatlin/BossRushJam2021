using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Image background;
    [SerializeField] AK.Wwise.Event[] PlayOnStart;
    [SerializeField] AK.Wwise.Event[] StopOnDestroy;

    private void Start()
    {
        for (int i = 0; i < PlayOnStart.Length; i++)
        {
            if(PlayOnStart[i] != null) { PlayOnStart[i].Post(player); }
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < StopOnDestroy.Length; i++)
        {
            if (StopOnDestroy[i] != null) { StopOnDestroy[i].Post(player); }
        }
    }

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
        if(background)
        {
            background.color = Random.ColorHSV();
            var imageAlpha = background.color;
            imageAlpha.a = 0.3f;
            background.color = imageAlpha;
        }
    }

    public void SetAudioParameter(string parameter, float value)
    {
        AkSoundEngine.SetRTPCValue(parameter, value);
    }
}
