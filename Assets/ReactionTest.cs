using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class ReactionTest : TestingMode
{
    public enum State
    {
        Static,
        WaitForFlash
    }

    protected State currentState = State.Static;
    protected float targetTime = 0.0f;
    protected List<float> testTimes = new List<float>();
    public Camera c;

    public override string ModeName()
    {
        return "Reaction Test";
    }

    public bool IsActivateKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }

        for (KeyCode c = KeyCode.Joystick1Button0; c <= KeyCode.Joystick4Button19; c++)
        {
            if (Input.GetKeyDown(c))
            {
                return true;
            }
        }
        return false;
    }

    void StartTest()
    {
        targetTime = Time.unscaledTime + UnityEngine.Random.Range(2.0f, 4.0f);
        this.currentState = State.WaitForFlash;
    }


    void PassTest()
    {
        float result = Time.unscaledTime - targetTime;
        testTimes.Insert(0,result);
        this.currentState = State.Static;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActivateKeyDown())
        {
            if (currentState == State.Static)
            {
                StartTest();
            }
            else if (currentState == State.WaitForFlash)
            {
                PassTest();
            }
        }

        if (currentState == State.Static)
        {
            c.backgroundColor = Color.white;
        }
        if (currentState == State.WaitForFlash)
        {
            if (Time.unscaledTime < this.targetTime)
            {
                c.backgroundColor = Color.black;
            }
            else
            {
                c.backgroundColor = Color.white;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            this.testTimes.Clear();
        }
    }

    protected override void OnGUI()
    {
        base.OnGUI();
        if (this.currentState == State.Static)
        {
            DrawResults();
            DrawInstructions();
        }
    }


    protected void DrawInstructions()
    {
        int w = Screen.width, h = Screen.height;
        Rect rect = new Rect(0, 0, w, h);
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.MiddleLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        string text = "Press space to go to next mode, then press space when screen flashes white. \n" +
                      "Use R to reset stats\n" +
                      "You can also use joystick keys";

        GUI.Label(rect, text, style);
    }
    protected void DrawResults()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h);
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        StringBuilder sb = new StringBuilder();

        int index = 0;
        float? avg5 = 0.0f;
        foreach (float f in testTimes)
        {
            if (index < 5)
            {
                if (f >= 0.0 && avg5.HasValue)
                {
                    avg5 += f;
                } else
                {
                    avg5 = null;
                }                
            }
            sb.Append(f);
            sb.Append(Environment.NewLine);
            index++;
        }
        sb.Append("Average of 5: " + (avg5.HasValue ? (avg5 / 5.0f).ToString() : "Invalid"));
        string text = sb.ToString();
        GUI.Label(rect, text, style);
    }

}
