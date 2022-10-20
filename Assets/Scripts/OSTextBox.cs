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

#pragma warning disable CS0414
    [Multiline]
    [SerializeField]
    string _windowsText = null;

    [Multiline]
    [SerializeField]
    string _nonWindowsText = null;
#pragma warning restore CS0414

    // Start is called before the first frame update
    void Start()
    {
        string finalText = _baseText;
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        finalText += "\n\n" + _windowsText;
#else
        finalText += "\n\n" + _nonWindowsText;
#endif
        _text.text = finalText;
    }
}
