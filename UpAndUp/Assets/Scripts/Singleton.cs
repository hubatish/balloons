using UnityEngine;


/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// Base code from Unity wiki
/// </summary>

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (alreadyDestroyed)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                             "with object:"+_instance.gameObject.name);
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        Debug.Log("[Singleton] An instance of " + typeof(T) +
                            " is needed in the scene, so '" + singleton +
                            "' was created.");
                    }
                    else
                    {
                          //DontDestroyOnLoad(_instance.gameObject);
                    }
                }

                return _instance;
            }
        }
    }

    protected static bool alreadyDestroyed = false;
    public bool dontDestroyOnLoad = false;
    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    public void OnApplicationQuit()
    {
        //ZH 3-13 changed onApplicationQuit to alreadyDestroyed, so works for changing/restarting levels (without DontDestroyOnLoad) as well as quitting game
        alreadyDestroyed = true;
    }

    public void OnDestroy()
    {
        alreadyDestroyed = true;
    }

    protected virtual void OnLevelWasLoaded(int level)
    {
        alreadyDestroyed = false;
    }

    /// <summary>
    /// applicationIsQuitting was getting set to true and never being reset to false
    /// We do that here
    /// Also, by default old gameobjects are deleted at end of scene, but if one carried over destroy the new one (corresponding to just traveled to scene)
    /// </summary>
    public virtual void Awake()
    {
        alreadyDestroyed = false;
        if(Instance!=null)
        {
            //There are multiple of this singleton!  Panic and kill self (as I am the new one)
            if (FindObjectsOfType(typeof(T)).Length > 1)
            {
                GameObject.Destroy(gameObject);
            }
        }
        if(dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
