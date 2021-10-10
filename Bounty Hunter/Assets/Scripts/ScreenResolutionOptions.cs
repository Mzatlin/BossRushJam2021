using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenResolutionOptions : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        Resolution currentRes;
        int index = 0;
        foreach(var res in resolutions)
        {
            options.Add(res.width + " x " + res.height);
            if(res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
            {
                currentRes = res;
            }
            else
            {
                index++;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = index;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
