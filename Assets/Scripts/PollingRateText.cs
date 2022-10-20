using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PollingRateText : MonoBehaviour
{
    Text t;
    public string preText = "Keyboard Polling rate: \n";
    public PollingRateChecker pollingRateChecker;

    // Use this for initialization
    void Start()
    {
        t = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        t.text = preText + pollingRateChecker.LastKeyboardInputString;
    }
}
