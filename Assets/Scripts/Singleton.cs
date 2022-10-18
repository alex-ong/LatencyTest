using UnityEngine;
using UnityEngine.SceneManagement;

// basic singleton for MonoBehaviours
// TODO: remove CreateInstance
// TODO: instantiate/destroy on Instance by using reflection to check top level SceneDependencies & Persistance
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static T m_Instance;

    // true to set DoNotDestroyOnLoad for the given object
    public static bool Persistent
    {
        get => true;
    }

    // if null then SceneSingleton unusable
    public static string[] SceneDependencies
    {
        get => null;
    }

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                return null;
            }

            lock (m_Lock)
            {
                if (m_Instance == null)
                    m_Instance = (T)FindObjectOfType(typeof(T));
                return m_Instance;
            }
        }
    }

    /*
        Creates the Singleton from 'SceneSingletonTest'
        TODO: Replace by adding comp in SceneSingletonListener
    */
    public static void CreateInstance(GameObject obj)
    {
        m_Instance = obj.AddComponent<T>();
    }

    public virtual void Awake()
    {
        m_Instance = GetComponent<T>();
    }

    public virtual void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }

    public virtual void OnDestroy()
    {
        m_ShuttingDown = true;
    }
}
