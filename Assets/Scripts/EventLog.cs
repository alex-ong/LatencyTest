using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class EventLog : MonoBehaviour
{
    public List<string> events;
    public const int MAX_EVENTS = 36;
    public string eventString { get { return string.Join("\n", events.ToArray()); } }

    private Text t;
    // Use this for initialization
    void Start()
    {
        this.t = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        bool eventAdded = false;
        for (int i = (int)KeyCode.A; i <= (int)KeyCode.Z; i++)
        {
            if (CheckKey((KeyCode)i)) {
                eventAdded = true;
            }
        }

        for (int i = (int)KeyCode.JoystickButton0; i <= (int)KeyCode.JoystickButton19; i++)
        {
            if (CheckKey((KeyCode)i))
            {
                eventAdded = true;
            }
        }

        if (eventAdded) {
            int numElementsToKeep = Math.Min(MAX_EVENTS, events.Count);
            events = events.GetRange(events.Count - numElementsToKeep, numElementsToKeep);            
            this.t.text = eventString;
        }
        
    }

    private bool CheckKey(KeyCode kc)
    {
        bool result = false;
        if (Input.GetKeyDown(kc))
        {
            events.Add("KeyDown: " + kc.ToString() + " " + Time.unscaledTime.ToString());
            result = true;
        }
        if (Input.GetKeyUp(kc))
        {
            events.Add("KeyUp  : " + kc.ToString() + " " + Time.unscaledTime.ToString());
            result  = true;
        }
        return result;
    }
}
