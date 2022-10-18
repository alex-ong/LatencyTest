using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets max queued frames to 0 and target frame rate to maximum
/// </summary>
public class MaxQueuedFrames : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.maxQueuedFrames = 0;
        Application.targetFrameRate = 0;
    }
}
