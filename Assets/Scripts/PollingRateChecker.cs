using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollingRateChecker : MonoBehaviour
{
    public float lastKeyboardInput = 0.0f;
    public float? minimumTime = null;
    public string lastKeyboardInputString = "";
    public KeyCode startCode;
    public KeyCode endCode;

    // Update is called once per frame
    void Update()
    {
        bool keyDetected = ProcessNativeUnityInput();
        if (keyDetected)
        {
            lastKeyboardInput = Time.unscaledTime;
        }
    }

    /// <summary>
    /// Processes input via Unity's OnGUI System, which is meant to be more accurate
    /// Spoiler: it's not
    /// </summary>
    /// <returns></returns>
    bool ProcessOnGUIEvents()
    {
        var events = HighFreqInput.Instance.GetEvents();

        bool keyDetected = false;
        foreach (var inputEvent in events)
        {
            if (!inputEvent.keyUp && ValidCode(inputEvent.keyCode))
            {
                float minTime = inputEvent.time - lastKeyboardInput;
                keyDetected = true;
                if (minimumTime.HasValue && minTime < minimumTime.Value || !minimumTime.HasValue)
                {
                    minimumTime = minTime;
                    this.lastKeyboardInputString = (minimumTime * 1000).ToString() + " ms";
                }
            }
        }
        return keyDetected;
    }

    /// <summary>
    /// Processes input via Unity's native system
    /// </summary>
    /// <returns></returns>
    bool ProcessNativeUnityInput()
    {
        bool keyDetected = false;
        for (int i = (int)startCode; i <= (int)endCode; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                float minTime = Time.unscaledTime - lastKeyboardInput;
                keyDetected = true;
                if (minimumTime.HasValue && minTime < minimumTime.Value || !minimumTime.HasValue)
                {
                    minimumTime = minTime;
                    this.lastKeyboardInputString = (minimumTime * 1000).ToString() + " ms";
                }
                break;
            }
        }
        return keyDetected;
    }

    bool ValidCode(KeyCode kc)
    {
        return startCode <= kc && kc <= endCode;
    }
}
