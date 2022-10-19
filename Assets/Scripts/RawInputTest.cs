using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityRawInput;

public class RawInputTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RawInput.Start(true);
    }

    void OnDisable()
    {
        RawInput.Stop();
    }
}
