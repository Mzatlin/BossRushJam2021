using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "VolumeSettings")]
public class VolumeSettingsSO : ScriptableObject
{
    public float masterVolume = 100f;
    public float SFXVolume = 55f;
    public float musicVolume = 45f;
}
