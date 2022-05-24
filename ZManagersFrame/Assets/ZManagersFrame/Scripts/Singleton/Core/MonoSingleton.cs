
using UnityEngine;


namespace DesignPattern.SingleTon
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField]
        protected bool dontDestroyOnLoad = false;
        private static T instance;
        private static object _lock = new object();
        private static bool _applicationIsQuitting = false;
        public static T Instance
        {
            get
            {
                if (_applicationIsQuitting)
                {
                    return null;
                }
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = (T)FindObjectOfType(typeof(T));
                        if (FindObjectsOfType(typeof(T)).Length > 2)
                        {
                            Debug.LogError("there are two instance in the scene!");
                        }
                        if (instance == null)
                        {
                            GameObject singleton = new GameObject();
                            instance = singleton.AddComponent<T>();
                            singleton.name = "(singleton) " + typeof(T).ToString();
                        }
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
                if (dontDestroyOnLoad)
                {
                    //DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                if (instance != this)
                {
                    Destroy(this);
                    Debug.LogError("there are two instance in the scene!");
                    return;
                }
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
                //_applicationIsQuitting = true;
            }
        }
    }
}
