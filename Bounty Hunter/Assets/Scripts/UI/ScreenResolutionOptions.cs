using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

using ScreenDimensions = System.Tuple<int, int>;

public class ScreenResolutionOptions : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    HashSet<ScreenDimensions> uniqResolutions = new HashSet<ScreenDimensions> ();
    Dictionary<ScreenDimensions, int> maxRefreshRates = new Dictionary<ScreenDimensions, int>();
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
            ScreenDimensions resolution = new ScreenDimensions(res.width, res.height);
            uniqResolutions.Add(resolution);
            if (maxRefreshRates.ContainsKey(resolution))
            {
                maxRefreshRates[resolution] = res.refreshRate;
            }
            else
            {
                maxRefreshRates.Add(resolution, res.refreshRate);
            }
        }
        foreach(ScreenDimensions resolution in uniqResolutions)
        {
            Resolution res = new Resolution();
            res.width = resolution.Item1;
            res.height = resolution.Item2;
            if(maxRefreshRates.TryGetValue(resolution,out int refreshRate))
            {
                res.refreshRate = refreshRate;
            }

            options.Add(res.width + " x " + res.height);
            if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
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
