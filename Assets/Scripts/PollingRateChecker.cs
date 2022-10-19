using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRawInput;

public class PollingRateChecker : MonoBehaviour
{
    public float lastKeyboardInput = 0.0f;
    public float? minimumTime = null;
    public string lastKeyboardInputString = "";
    public KeyCode startCode;
    public KeyCode endCode;

    private enum CheckType
    {
        RAW_INPUT,
        UNITY,
        ONGUI
    }

    [SerializeField]
    private CheckType RateCheckType = CheckType.UNITY;

    void Start()
    {
        if (RateCheckType == CheckType.RAW_INPUT)
        {
            RawInput.OnKeyDown += RawInput_OnKeyDown;
            RawInput.OnKeyUp += RawInput_OnKeyDown;
        }
    }

    void OnDisable()
    {
        if (RateCheckType == CheckType.RAW_INPUT)
        {
            RawInput.OnKeyDown -= RawInput_OnKeyDown;
        }
    }

    /// <summary>
    /// Converts rawkey to keycode. Only works for rk in range A-Z
    /// </summary>
    /// <param name="rk"></param>
    /// <returns></returns>
    protected bool RawKeyToKeyCode(RawKey rk, out KeyCode result)
    {
        if (RawKey.A <= rk && rk <= RawKey.Z)
        {
            result = (rk - RawKey.A) + KeyCode.A;
            return true;
        }
        result = KeyCode.None;
        return false;
    }

    private void RawInput_OnKeyDown(RawKey rk)
    {
        KeyCode kc;
        var currentTime = Time.realtimeSinceStartup;
        if (RawKeyToKeyCode(rk, out kc) && ValidCode(kc))
        {
            float minTime = currentTime - lastKeyboardInput;
            if (minimumTime.HasValue && minTime < minimumTime.Value || !minimumTime.HasValue)
            {
                minimumTime = minTime;
                this.lastKeyboardInputString = (minimumTime * 1000).ToString() + " ms";
            }
            lastKeyboardInput = currentTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (RateCheckType != CheckType.RAW_INPUT)
        {
            bool keyDetected = ProcessNativeUnityInput();
            if (keyDetected)
            {
                lastKeyboardInput = Time.realtimeSinceStartup;
            }
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
                float minTime = Time.realtimeSinceStartup - lastKeyboardInput;
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
