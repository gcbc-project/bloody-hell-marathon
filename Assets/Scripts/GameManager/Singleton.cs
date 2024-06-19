using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    [SerializeField]
    private bool canDestroyOnLoad = true;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance != null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    instance= go.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            if (canDestroyOnLoad)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
