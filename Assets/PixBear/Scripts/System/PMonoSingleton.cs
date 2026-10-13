using UnityEngine;

namespace PB.SYSTEM
{
    [DefaultExecutionOrder(-1000)]
    public class PMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private bool destroyOnLoad = true;
        private static T singleton;
        public static T Singleton
        {
            get
            {
                if (singleton == null)
                {
#if UNITY_2023_2_OR_NEWER
                    singleton = FindAnyObjectByType<T>();
#elif UNITY_2022_3_OR_NEWER
                singleton = FindObjectOfType<T>();
#endif

                    if (singleton == null)
                    {
                        GameObject obj = new GameObject(typeof(T).Name);
                        singleton = obj.AddComponent<T>();
                    }
                }

                return singleton;
            }
        }

        protected virtual void Awake()
        {
            if (singleton == null)
            {
                singleton = this as T;
                if (!destroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                // Destroy(gameObject);
            }
        }
    }
}
