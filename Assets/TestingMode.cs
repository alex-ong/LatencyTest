using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TestingMode : MonoBehaviour
{
    public abstract string ModeName();

    public virtual void DrawModeName()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperCenter;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        string text = ModeName();
        GUI.Label(rect, text, style);        
    }

    protected virtual void OnGUI()
    {
        DrawModeName();
    }
}
