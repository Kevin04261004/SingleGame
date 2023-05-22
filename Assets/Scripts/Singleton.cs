using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public T instance { get; protected set; } = null;
    protected virtual void Awake()
    {
        if (instance != null && instance != this) { Destroy(gameObject); return; }
        instance = GetComponent<T>();
    }
}
