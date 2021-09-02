using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenOptions : MonoBehaviour
{
    public void SetFullScreen(bool isfullScreen)
    {
        Screen.fullScreen = isfullScreen;
    }
}
