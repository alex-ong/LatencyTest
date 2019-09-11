using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSText : MonoBehaviour {
    public HUDFPS hudfps;
    private Text t;
	// Use this for initialization
	void Start () {
        t = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
         t.text = "GamePollingRate: \n" + hudfps.FPSString + "hz";
    }
}
