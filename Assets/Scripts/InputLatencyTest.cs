using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLatencyTest : TestingMode
{
    public Camera c;
    public const string modeName = "InputLatencyTest. Press space to flash screen";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool white = false;
        for (KeyCode c = KeyCode.Joystick1Button0; c <= KeyCode.Joystick4Button19; c++)
        {
            if (Input.GetKey(c))
            {
                white = true;                
            }
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            white = true;
        }

        if (white) 
        {
            c.backgroundColor = Color.white;
        } else
        {
            c.backgroundColor = new Color(0, 0, 0);
        }
    }

    
    public override string ModeName()
    {
        return modeName;
    }

}
