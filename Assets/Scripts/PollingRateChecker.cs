using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollingRateChecker : MonoBehaviour {
    public float lastKeyboardInput = 0.0f;
    public float? minimumTime = null;
    public string lastKeyboardInputString = "";
    public KeyCode startCode;
    public KeyCode endCode;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //check alphas.
        bool keyDetected = false;
        for (int i = (int)startCode; i <= (int)endCode; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                float minTime = Time.unscaledTime - lastKeyboardInput;
                keyDetected = true;
                if (minimumTime.HasValue && minTime < minimumTime.Value ||  !minimumTime.HasValue)
                {
                    minimumTime = minTime;
                    this.lastKeyboardInputString = (minimumTime * 1000).ToString() + " ms";
                }
                break;
            }
        }
        

        if (keyDetected)
        {
            lastKeyboardInput = Time.unscaledTime;            
        }

	}
}
