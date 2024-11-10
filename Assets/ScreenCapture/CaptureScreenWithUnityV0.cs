using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureScreenWithUnityV0 : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("Screen", 0, 1);
    }

    void Screen()
    {
        ScreenCapture.CaptureScreenshot("SomeLevel.png");

    }
}
