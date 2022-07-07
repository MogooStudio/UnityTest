using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyFramework
{
    public abstract class Singleton<T> where T : new()
    {
        private static object m_lock = new object();
        private static T m_instance;

        public static T Instance()
        {
            if (m_instance == null)
            {
                lock (m_lock)
                {
                    if (m_instance == null)
                    {
                        m_instance = new T();
                    }
                }
            }
            return m_instance;
        }
    }

    public class UnitySingleton<T> : MonoBehaviour where T : Component
    {
        private static T m_instance;

        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = FindObjectOfType(typeof(T)) as T;
                    if (m_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.hideFlags = HideFlags.HideAndDontSave;
                        m_instance = (T)obj.AddComponent(typeof(T));
                    }
                }
                return m_instance;
            }
        }

        public virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (m_instance == null)
            {
                m_instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}


