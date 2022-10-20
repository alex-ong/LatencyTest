using UnityEngine;

public class RawInputDaemon : MonoBehaviour
{
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
    // Start is called before the first frame update
    void Start()
    {
        UnityRawInput.RawInput.Start(true);
    }

    void OnDisable()
    {
        UnityRawInput.RawInput.Stop();
    }
#endif
}
