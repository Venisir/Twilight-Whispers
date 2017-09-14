using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : class, new()
{
    public static T Instance {
        get {
            return sInstance;
        }
    }

    public virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (sInstance == null)
        {
            sInstance = this as T;
        }
        else
            Debug.Log(name + ": Error: already initialized");
    }

    private static T sInstance = null;
}
