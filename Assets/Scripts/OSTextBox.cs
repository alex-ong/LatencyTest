using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OSTextBox : MonoBehaviour
{
    [SerializeField]
    Text _text = null;

    [SerializeField]
    [Multiline]
    string _baseText = null;

    [Multiline]
    [SerializeField]
    string _windowsText = null;

    [Multiline]
    [SerializeField]
    string _nonWindowsText = null;

    // Start is called before the first frame update
    void Start()
    {
        string finalText = _baseText;
        if (
            Application.platform == RuntimePlatform.WindowsPlayer
            || Application.platform == RuntimePlatform.WindowsEditor
        )
        {
            finalText += "\n\n" + _windowsText;
        }
        else
        {
            finalText += "\n\n" + _nonWindowsText;
        }
        _text.text = finalText;
    }
}
