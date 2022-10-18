using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Stolen from https://gist.github.com/areyoutoo/11021717
/// This is meant to allow subframe accuracy on events,
/// but it does not work in Unity 2019.2.12f1
/// </summary>
public class HighFreqInput : Singleton<HighFreqInput>
{
    public struct InputEvent
    {
        public readonly bool keyUp;
        public readonly KeyCode keyCode;
        public readonly int frame;
        public readonly float time;

        public readonly string msg;

        public InputEvent(bool ku, KeyCode kc, int f, float t)
        {
            keyUp = ku;
            keyCode = kc;
            frame = f;
            time = t;

            msg = string.Format(
                "{0} {1} | frame {2} | time {3}",
                kc.ToString(),
                ku ? "up" : "down",
                f,
                t
            );
        }
    }

    List<InputEvent> events = new List<InputEvent>();

    /// <summary>
    /// This grabs each input event during OnGUI, which refreshes much more often than Update()
    /// </summary>
    void OnGUI()
    {
        Event e = Event.current;

        if (e.isKey)
        {
            switch (e.type)
            {
                case EventType.KeyDown:
                    events.Add(
                        new InputEvent(false, e.keyCode, Time.frameCount, Time.realtimeSinceStartup)
                    );
                    break;

                case EventType.KeyUp:
                    events.Add(
                        new InputEvent(true, e.keyCode, Time.frameCount, Time.realtimeSinceStartup)
                    );
                    break;
            }
        }
    }

    /// <summary>
    /// Clear the events every frame
    /// </summary>
    public void LateUpdate()
    {
        events.Clear();
    }

    /// <summary>
    /// Grabs all events, and clears current eventn queue
    /// </summary>
    /// <returns></returns>
    public List<InputEvent> GetEvents()
    {
        var result = new List<InputEvent>(events);
        return result;
    }
}
