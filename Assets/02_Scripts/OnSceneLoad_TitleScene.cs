using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneLoad_TitleScene : MonoBehaviour
{
    public GameObject go;
    private void Awake()
    {
        DontDestroyOnLoad(go);
    }
}
