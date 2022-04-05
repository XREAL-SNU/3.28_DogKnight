namespace MainSystem
{
    using UnityEngine;

    public class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {

        private static T _Instance = null;                      
        private static object _lock = new object();             
        private static bool applicationIsQuitting = false;      

        public static T Instance
        {   
            get
            {
                if (applicationIsQuitting)
                { 
#if UNITY_EDITOR	
                    Debug.LogWarning("[Singleton] Instance" + typeof(T) + "Is Already");
#endif
                    return null;
                }

                lock (_lock)
                {   
                    if (null == _Instance)
                    {   
                        _Instance = (T)FindObjectOfType(typeof(T));

                        
                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {   
#if UNITY_EDITOR
                            Debug.LogError("[Singleton] Already Instance : ERROR more then 2");
#endif
                            return _Instance;
                        }

                        if (null == _Instance)
                        {   
                            GameObject singleton = new GameObject();
                            _Instance = singleton.AddComponent<T>();
                            singleton.name = "(singleton)" + typeof(T).ToString();

                            DontDestroyOnLoad(singleton);
#if UNITY_EDITOR
                            Debug.Log("[Singleton] An Instance of " + typeof(T).ToString());
#endif

                        }
#if UNITY_EDITOR
                        else
                        {

                            Debug.Log("[Singleton] Using Instance Already Created: " + _Instance.gameObject.name);

                        }
#endif
                    }

                    return _Instance;       
                }
            } 
        }

        public void OnDestroy()
        {   
            applicationIsQuitting = true;
        }

        public void OnDestroyInstance()
        {
            Destroy(_Instance);
        }
    }

}
