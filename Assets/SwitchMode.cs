using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMode : MonoBehaviour
{
    public List<GameObject> modes;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        this.modes[index].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            this.modes[index].SetActive(false);
            index++;
            index %= this.modes.Count;
            this.modes[index].SetActive(true);
        }
    }
}
